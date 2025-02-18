using System;
using System.Collections.Generic;
using System.Linq;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class SelectionManager : Singleton<SelectionManager>
{
    public Action OnSelectionStarted;

    public Action OnSelectionCompleted;

    public Action<List<BoardSpace>> OnNextSelectionComplete;

    private Stack<GridSelectable> currentSelections = new Stack<GridSelectable>();

    public SelectionCriteria SelectionInProgress { get; private set; } = null;

    public void BeginSelection() {
        BeginSelection(new SelectionCriteria());
    }
        

    public void BeginSelection(SelectionCriteria selectionCriteria) {
        
        if(SelectionInProgress != null) {
            Debug.LogWarning("Trying to begin a selection when current selection is in progress");
            return;
        }

        SelectionInProgress = selectionCriteria;
        currentSelections = new Stack<GridSelectable>();

        List<BoardSpace> selectableSpaces = new List<BoardSpace>();

        BoardManager.Instance.ForEachSpace((space) => {
            
            bool isSelectable = SelectionInProgress.Filter.Invoke(space);
            space.Selectable.SetSelectable(isSelectable);

            if(isSelectable) {
                selectableSpaces.Add(space);
            }

        });


        // "Guess" the selection if the number of selectable spaces
        // is less than the number of spaces we can select
        if(selectableSpaces.Count <= SelectionInProgress.MaxSelections) {
            foreach(BoardSpace space in selectableSpaces) {
                CheckSelection(space.Selectable);
            }
        }

        OnSelectionStarted?.Invoke();
    }

    public void CheckSelection(GridSelectable gridSelectable)
    {
        if(SelectionInProgress == null || currentSelections.Count >= SelectionInProgress.MaxSelections) {
            return;
        }

        currentSelections.Push(gridSelectable);
        gridSelectable.Select();
    }

    public void Deselect()
    {
        if(SelectionInProgress == null || currentSelections.Count <= 0) {
            return;
        }

        GridSelectable selectable = currentSelections.Pop();
        selectable.Deselect();
    }

    public bool ValidSelection() {
        return SelectionInProgress != null &&
            currentSelections.Count >= SelectionInProgress.MinSelections &&
            currentSelections.Count <= SelectionInProgress.MaxSelections;
    }

    public void ProcessSeletion() {

        if(SelectionInProgress == null || !ValidSelection()) {
            return;
        }

        List<BoardSpace> selections = currentSelections.Select(x => x.Space).ToList();
        
        ResetSelections();
        
        OnSelectionCompleted?.Invoke();

        OnNextSelectionComplete?.Invoke(currentSelections.Select(x => x.Space).ToList());
        OnNextSelectionComplete = null;
    }

    private void ResetSelections()
    {
        if(SelectionInProgress == null) {
            return;
        }

        BoardManager.Instance.ForEachSpace((space) => {
            space.Selectable.Deselect();
            space.Selectable.SetSelectable(false);
        });

        SelectionInProgress = null;
    }

    public void Update() {
        CheckForDeselect();
    }

    private void CheckForDeselect()
    {
        if(Input.GetMouseButtonDown(1)) {
            Deselect();
        }
    }

    public void CancelCurrentSelection() {
        ResetSelections();
        OnSelectionCompleted?.Invoke();
        OnNextSelectionComplete = null;
    }

    public void SkipCurrentSelection() {
        ResetSelections();

        OnSelectionCompleted?.Invoke();

        OnNextSelectionComplete?.Invoke(new List<BoardSpace>());
        OnNextSelectionComplete = null;
    }

    [ProButton]
    private void TestStartSelection() {

        CancelCurrentSelection();

        SelectionCriteria criteria = new SelectionCriteria()
            .WithMaxSelections(5)
            .WithFilter((space) => {
                return space.IsOccupied;
            });
        
        BeginSelection(criteria);
    }

    [ProButton]
    private void TestStopSelection() {
        CancelCurrentSelection();
    }

    [ProButton]
    private void TestProcessSelection() {
        ProcessSeletion();
    }
}

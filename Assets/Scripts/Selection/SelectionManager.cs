using System;
using System.Collections.Generic;
using System.Linq;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class SelectionManager : Singleton<SelectionManager>
{
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
    
        BoardManager.Instance.ForEachSpace((space) => {
            space.Selectable.SetSelectable(SelectionInProgress.Filter.Invoke(space));
        });

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

        OnNextSelectionComplete?.Invoke(currentSelections.Select(x => x.Space).ToList());
        CancelCurrentSelection();
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
        
        if(SelectionInProgress == null) {
            return;
        }

        BoardManager.Instance.ForEachSpace((space) => {
            space.Selectable.Deselect();
            space.Selectable.SetSelectable(false);
        });

        SelectionInProgress = null;
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

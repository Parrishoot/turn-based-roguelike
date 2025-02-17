using UnityEngine;

public class PlayerSelectionController : ISelectionController
{
    public void BeginSelection(SelectionProcessor selectionProcessor)
    {
        SelectionManager.Instance.OnNextSelectionComplete += selectionProcessor.ProcessSelection;
        SelectionManager.Instance.BeginSelection(selectionProcessor.GetCriteria());
    }
}

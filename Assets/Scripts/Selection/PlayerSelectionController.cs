using UnityEngine;

public class PlayerSelectionController : SelectionController
{
    public override void BeginSelection(SelectionProcessor selectionProcessor)
    {
        SelectionManager.Instance.OnNextSelectionComplete += selectionProcessor.ProcessSelection;
        SelectionManager.Instance.BeginSelection(selectionProcessor.GetCriteria());
    }
}

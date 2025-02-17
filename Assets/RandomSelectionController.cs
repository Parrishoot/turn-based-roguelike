using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class RandomSelectionController : ISelectionController
{
    public void BeginSelection(SelectionProcessor selectionProcessor)
    {
        List<BoardSpace> applicableBoardSpaces = BoardManager.Instance.GetMatchingSpaces(selectionProcessor.GetCriteria().Filter);
        Queue<BoardSpace> selectionQueue = new Queue<BoardSpace>(applicableBoardSpaces.Shuffled());

        List<BoardSpace> selections = new List<BoardSpace>();
        int numSelections = Random.Range(selectionProcessor.GetCriteria().MinSelections, selectionProcessor.GetCriteria().MaxSelections);

        while(numSelections > 0 && selectionQueue.Count > 0) {
            selections.Add(selectionQueue.Dequeue());
        }

        selectionProcessor.ProcessSelection(selections);
    }
}

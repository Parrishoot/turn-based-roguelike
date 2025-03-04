using System.Collections.Generic;
using UnityEngine;

public class SpawnSelectionProcessor : SelectionProcessor
{
    public override SelectionCriteria GetCriteria()
    {
        return new SelectionCriteria()
            .WithFilter(space => {
                return !space.IsOccupied;
            });
    }

    public override void ProcessSelection(List<BoardSpace> selectedSpaces)
    {
        foreach (BoardSpace space in selectedSpaces) {
            SpawnManager.Instance.SpawnAtSpace(space);
        }
    }
}

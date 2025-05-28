using System.Collections.Generic;
using UnityEngine;

public class SpawnSelectionProcessor : SelectionProcessor
{
    private PlayerClass playerClass;

    public SpawnSelectionProcessor(PlayerClass playerClass)
    {
        this.playerClass = playerClass;
    }

    public override SelectionCriteria GetCriteria()
    {
        return new SelectionCriteria()
            .WithFilter(space =>
            {
                return !space.IsOccupied;
            });
    }

    public override void ProcessSelection(List<BoardSpace> selectedSpaces)
    {
        foreach (BoardSpace space in selectedSpaces) {
            // TODO: Set this to the right character class
            SpawnManager.Instance.SpawnPlayerCharacter(playerClass, space);
        }
    }
}

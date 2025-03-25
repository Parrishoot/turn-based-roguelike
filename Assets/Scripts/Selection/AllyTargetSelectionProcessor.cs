using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class AllyTargetSelectionProcessor : SelectionProcessor
{
    protected int NumTargets { get; private set; } = 1;

    protected int Range { get; private set; } = 1;

    private CharacterManager characterManager;

    public AllyTargetSelectionProcessor(CharacterManager characterManager, int numTargets = 1, int range = 1)
    {
        this.NumTargets = numTargets;
        this.Range = range;
        this.characterManager = characterManager;
    }

    public override SelectionCriteria GetCriteria()
    {
        int totalRange = characterManager.ProfileManager.ModifiedValue(CharacterStatType.RANGE, Range);

        return new SelectionCriteria()
            .WithMaxSelections(NumTargets)
            .WithFilter(space => {
                return space.DistanceTo(characterManager.Space, true) <= totalRange &&
                    space.IsOccupied &&
                    characterManager.GetCharacterType()
                                    .GetAlliedCharacterTypes()
                                    .Contains(space.Occupant.GetCharacterType());
            });
    }

    public override void ProcessSelection(List<BoardSpace> selectedSpaces)
    {
        List<BoardSpace> affectedSpaces = new List<BoardSpace>();

        foreach(BoardSpace space in selectedSpaces) {

            if(!space.IsOccupied) {
                Debug.LogWarning("Unable to damage character on this space - no occupant present");
                continue;
            }

            ApplyToSelectedSpace(space);
            affectedSpaces.Add(space);
        }

        OnSelectionProcessed?.Invoke(affectedSpaces);
    }

    protected abstract void ApplyToSelectedSpace(BoardSpace space);
}

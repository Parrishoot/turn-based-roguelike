using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackSelectionProcessor : SelectionProcessor
{
    private int numTargets = 1;

    private int damage = 1;

    private int range = 1;

    private CharacterManager characterManager;

    public AttackSelectionProcessor(CharacterManager characterManager, int numTargets = 1, int damage = 1, int range = 1)
    {
        this.numTargets = numTargets;
        this.damage = damage;
        this.range = range;
        this.characterManager = characterManager;
    }

    public override SelectionCriteria GetCriteria()
    {
        return new SelectionCriteria()
            .WithMaxSelections(numTargets)
            .WithFilter(space => {
                return space.DistanceTo(characterManager.Space, true) <= range &&
                    space.IsOccupied &&
                    characterManager.GetCharacterType()
                                    .GetOpposingCharacterTypes()
                                    .Contains(space.Occupant.GetCharacterType());
            });
    }

    public override void ProcessSelection(List<BoardSpace> selectedSpaces)
    {
        foreach(BoardSpace space in selectedSpaces) {
            
            if(!space.IsOccupied) {
                Debug.LogWarning("Unable to damage character on this space - no occupant present");
                continue;
            }

            space.Occupant.Damage(damage);

        }

        OnSelectionProcessed?.Invoke();
    }
}

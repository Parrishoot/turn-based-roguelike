using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatusEffectSelectionProcessor : SelectionProcessor
{
    private CharacterManager characterManager;

    private StatusEffectType statusEffectType;
    private int range = 1;
    
    private int numTargets = 1;

    public StatusEffectSelectionProcessor(CharacterManager characterManager, StatusEffectType statusEffectType, int range, int numTargets)
    {
        this.characterManager = characterManager;
        this.statusEffectType = statusEffectType;
        this.range = range;
        this.numTargets = numTargets;
    }

    public override SelectionCriteria GetCriteria()
    {
        int totalRange = characterManager.StatsManager.ModifiedValue(CharacterStatType.RANGE, range);
        StatusEffectController controller = statusEffectType.GetController(characterManager);
        CharacterType[] applicableCharacters = controller.Negative ? characterManager.GetCharacterType().GetOpposingCharacterTypes() :
                                                                     characterManager.GetCharacterType().GetAlliedCharacterTypes();


        return new SelectionCriteria()
            .WithMaxSelections(numTargets)
            .WithFilter(space => {
                return space.DistanceTo(characterManager.Space, true) <= totalRange &&
                    space.IsOccupied &&
                    applicableCharacters.Contains(space.Occupant.GetCharacterType()) &&
                    !space.Occupant.Immunities.Contains(statusEffectType);
            });
    }

    public override void ProcessSelection(List<BoardSpace> selectedSpaces)
    {
        foreach(BoardSpace space in selectedSpaces) {
            space.Occupant.ApplyStatus(statusEffectType);
        }
    }
}

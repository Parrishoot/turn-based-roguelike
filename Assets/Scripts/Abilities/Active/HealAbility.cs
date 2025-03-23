using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HealAbility", menuName = "Abilities/Active/HealAbility", order = 1)]
public class HealAbility : ActiveAbility
{
    [field:SerializeReference]
    public int Range { get; private set;}

    [field:SerializeReference]
    public int HealAmount { get; private set;}

    [field:SerializeReference]
    public int NumTargets { get; private set;}

    public override string GetAbilityDescription()
    {
        return string.Format("Heal {0} target(s) up to +{1} range for +{2} damage", NumTargets, Range, HealAmount);
    }

    public override ActiveAbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        ISelectionController selectionController = characterManager.GetAbilitySelectionController(GetAbilitySelectionCriteria(characterManager));
        SelectionProcessor selectionProcessor = new HealSelectionProcessor(characterManager, NumTargets, Range, HealAmount);

        return new SelectionAbilityProcessor(characterManager, selectionController, selectionProcessor);
    }

    public override AbilitySelectionCriteria GetDefaultAbilitySelectionCriteria(CharacterManager characterManager)
    {
        return new AbilitySelectionCriteria()
            .WithAbilityType(AbilityType.SUPPORT)
            .WithRangeToTarget(characterManager.ProfileManager.ModifiedValue(CharacterStatType.RANGE, Range));
    }
}

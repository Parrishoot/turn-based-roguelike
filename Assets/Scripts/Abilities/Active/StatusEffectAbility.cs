using UnityEngine;

[CreateAssetMenu(fileName = "StatusAbility", menuName = "Abilities/Active/StatusAbility", order = 1)]
public class StatusEffectAbility : ActiveAbility
{
    [field:SerializeReference]
    public StatusEffectType EffectType { get; private set; }

    [field:SerializeReference]
    public int Range { get; private set; } = 1;

    [field:SerializeReference]
    public int NumTargets { get; private set; } = 1;

    public override string GetAbilityDescription()
    {
        return string.Format("Apply {0} to {1} targets within range {2}", EffectType, NumTargets, Range);
    }

    public override ActiveAbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {   
        ISelectionController selectionController = Range == 0 ? new SelfSelectionController(characterManager) : characterManager.GetAbilitySelectionController(GetDefaultAbilitySelectionCriteria(characterManager));
        StatusEffectSelectionProcessor processor = new StatusEffectSelectionProcessor(characterManager, EffectType, Range, NumTargets);
        return new SelectionAbilityProcessor(characterManager, selectionController, processor);
    }

    public override AbilitySelectionCriteria GetDefaultAbilitySelectionCriteria(CharacterManager characterManager)
    {
        return new AbilitySelectionCriteria()
            .WithAbilityType(EffectType.GetController(characterManager).Negative ? AbilityType.OFFENSIVE : AbilityType.SUPPORT)
            .WithRangeToTarget(characterManager.ProfileManager.ModifiedValue(CharacterStatType.RANGE, Range));
    }
}

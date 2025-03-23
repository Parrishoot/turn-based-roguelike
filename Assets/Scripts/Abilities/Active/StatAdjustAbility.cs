using UnityEngine;

[CreateAssetMenu(fileName = "StatAdjustAbility", menuName = "Abilities/Active/StatAbility", order = 1)]
public class StatAdjustAbility : ActiveAbility
{
    [SerializeField]
    private CharacterStatType statType;

    [SerializeField]
    private ValueAdjuster.Type statAdjusterType = ValueAdjuster.Type.ADD;

    [SerializeField]
    private int adjustmentValue = 1;

    public override string GetAbilityDescription()
    {
        return string.Format("Increase {0} by {1} {2}", statType, statAdjusterType, adjustmentValue);
    }

    public override ActiveAbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        return new StatAdjustAbilityProcessor(characterManager, statType, new ValueAdjuster(adjustmentValue, statAdjusterType));
    }

    public override AbilitySelectionCriteria GetDefaultAbilitySelectionCriteria(CharacterManager characterManager)
    {
        return new AbilitySelectionCriteria()
            .WithAbilityType(AbilityType.OFFENSIVE);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "StatAdjustAbility", menuName = "Abilities/StatAbility", order = 1)]
public class StatAdjustAbility : Ability
{
    [SerializeField]
    private CharacterStatType statType;

    [SerializeField]
    private StatAdjuster.Type statAdjusterType = StatAdjuster.Type.ADD;

    [SerializeField]
    private int adjustmentValue = 1;

    public override string GetAbilityDescription()
    {
        return string.Format("Increase {0} by {1} {2}", statType, statAdjusterType, adjustmentValue);
    }

    public override AbilityProcessor GetAbilityProcessor(CharacterManager characterManager)
    {
        return new StatAdjustAbilityProcessor(characterManager, statType, new StatAdjuster(adjustmentValue, statAdjusterType));
    }
}

using UnityEngine;

public class AbilitySelectionCriteria
{
    public AbilityType CurrentAbilityType { get; private set; } = AbilityType.OFFENSIVE;

    public AbilityType OverallAbilityType { get; private set; } = AbilityType.OFFENSIVE;

    public int RangeToTarget { get; private set; } = 1;

    public AbilitySelectionCriteria WithOverallAbilityType(AbilityType overallAbilityType) {
        this.OverallAbilityType = overallAbilityType;
        return this;
    }

    public AbilitySelectionCriteria WithCurrentAbilityType(AbilityType currentAbilityType) {
        this.CurrentAbilityType = currentAbilityType;
        return this;
    }

    public AbilitySelectionCriteria WithAbilityType(AbilityType abilityType) {
        this.CurrentAbilityType = abilityType;
        this.OverallAbilityType = abilityType;
        return this;
    }

    public AbilitySelectionCriteria WithRangeToTarget(int rangeToTarget) {
        this.RangeToTarget = rangeToTarget;
        return this;
    }
}

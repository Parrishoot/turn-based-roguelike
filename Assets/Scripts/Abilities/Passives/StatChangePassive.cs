using UnityEngine;

public abstract class StatChangePassive<T> : PassiveAbility
where T: System.Enum
{
    [field:SerializeReference]
    public T StatType { get; private set; }

    [field:SerializeReference]
    public ValueAdjuster.Type StatAdjustType { get; private set; } = ValueAdjuster.Type.ADD;

    [field:SerializeReference]
    public int Amount { get; private set; }

    public override PassiveController GetController()
    {
        return GetStatPassiveController();
    }

    protected abstract StatChangePassiveController<T> GetStatPassiveController();

    public override string GetAbilityDescription()
    {
        return string.Format("Increase {0} by {1} {2}", StatType, StatAdjustType, Amount);
    }
}
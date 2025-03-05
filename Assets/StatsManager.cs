using System.Collections.Generic;
using UnityEngine;

public abstract class StatsManager<T> : MonoBehaviour
where T: System.Enum
{
    public Dictionary<T, AdjustableStat> Stats { get; private set; }

    private void Awake()
    {
        Stats = InitStats();
    }

    protected abstract Dictionary<T, AdjustableStat> InitStats();

    public int ModifiedValue(T statType, int baseValue) {
        return baseValue + Stats[statType].CurrentValue;
    }
}

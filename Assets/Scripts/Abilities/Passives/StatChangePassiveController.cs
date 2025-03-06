using System.Collections.Generic;
using UnityEngine;

public abstract class StatChangePassiveController<T> : PassiveController
where T: System.Enum
{
    protected T statType;

    protected StatAdjuster statAdjuster;


    public StatChangePassiveController(T statType, StatAdjuster statAdjuster)
    {
        this.statType = statType;
        this.statAdjuster = statAdjuster;
    }

    protected abstract List<StatsManager<T>> GetStatsManagers();

    protected override void ProcessDeactivation()
    {
        foreach(StatsManager<T> manager in GetStatsManagers()) {
            manager.Stats[statType].RemoveAdjuster(statAdjuster);
        }
    }

    protected override void ProcessActivation()
    {
        foreach(StatsManager<T> manager in GetStatsManagers()) {
            manager.Stats[statType].AddAdjuster(statAdjuster);
        }
    }
}

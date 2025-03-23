using System.Collections.Generic;
using UnityEngine;

public abstract class StatChangePassiveController<T> : PassiveController
where T: System.Enum
{
    protected T statType;

    protected ValueAdjuster statAdjuster;


    public StatChangePassiveController(T statType, ValueAdjuster statAdjuster)
    {
        this.statType = statType;
        this.statAdjuster = statAdjuster;
    }

    protected abstract List<StatsManager<T>> GetStatsManagers();

    protected override void ProcessDeactivation()
    {
        foreach(StatsManager<T> manager in GetStatsManagers()) {
            manager.Stats[statType].Modifier.RemoveAdjuster(statAdjuster);
        }
    }

    protected override void ProcessActivation()
    {
        foreach(StatsManager<T> manager in GetStatsManagers()) {
            manager.Stats[statType].Modifier.AddAdjuster(statAdjuster);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdjustableStat
{
    private int baseValue;

    public List<StatAdjuster> Adjusters { get; private set; } = new List<StatAdjuster>();

    public AdjustableStat(int baseValue)
    {
        this.baseValue = baseValue;
    }

    public int CurrentValue {

        get {
            int val = baseValue;

            foreach(StatAdjuster adjuster in Adjusters) {
                val = adjuster.Adjust(val);
            }

            return val;
        }
    }

    public void AddAdjuster(StatAdjuster adjuster) {
        Adjusters.Add(adjuster);
        Adjusters.Sort();
    }

    public void RemoveAdjuster(StatAdjuster adjuster) {
        Adjusters.Remove(adjuster);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    private List<ValueAdjuster> Adjusters { get; set; } = new List<ValueAdjuster>();

    public void AddAdjuster(ValueAdjuster adjuster) {
        Adjusters.Add(adjuster);
        Adjusters.Sort();
    }

    public void RemoveAdjuster(ValueAdjuster adjuster) {
        Adjusters.Remove(adjuster);
    }

    public int GetModifiedValue(int baseValue) {
        
        int val = baseValue;

        foreach(ValueAdjuster adjuster in Adjusters) {
            val = adjuster.Adjust(val);
        }

        return val;
    }
}

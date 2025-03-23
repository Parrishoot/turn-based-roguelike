using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdjustableStat
{
    private int baseValue;

    public Modifier Modifier { get; private set; } = new Modifier();

    public AdjustableStat(int baseValue)
    {
        this.baseValue = baseValue;
    }

    public int CurrentValue {

        get {
            return Modifier.GetModifiedValue(baseValue);
        }
    }
}

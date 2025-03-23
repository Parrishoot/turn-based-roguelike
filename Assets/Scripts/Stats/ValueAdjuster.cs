using System;
using UnityEngine;

// TODO: ADD MULTS HERE
public class ValueAdjuster: IComparable
{
    public enum Type {
        ADD,
        MULT
    }

    public Type AdjustmentType { get; private set; } = Type.ADD;

    public int AdjustmentValue { get; private set; } = 1;

    public ValueAdjuster(int adjustmentValue, Type adjustmentType=Type.ADD)
    {
        this.AdjustmentType = adjustmentType;
        this.AdjustmentValue = adjustmentValue;
    }

    public int Adjust(int current) {
        return AdjustmentType switch
        {
            Type.MULT => current * AdjustmentValue,
            Type.ADD or _ => current + AdjustmentValue,
        };
    }

    public int CompareTo(object obj)
    {
        ValueAdjuster other = (ValueAdjuster) obj;

        if(AdjustmentType == Type.ADD && other.AdjustmentType == Type.MULT) {
            return -1;
        }
        else if(AdjustmentType == Type.MULT && other.AdjustmentType == Type.ADD) {
            return 1;
        }
        else {
            return 0;
        }
    }
}

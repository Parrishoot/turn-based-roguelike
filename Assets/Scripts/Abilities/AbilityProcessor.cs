using System;
using UnityEngine;

public abstract class AbilityProcessor
{
    public Action OnAbilityFinish { get; private set; }

    public abstract void Process();
}

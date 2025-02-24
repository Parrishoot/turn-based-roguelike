using System;
using UnityEngine;

public class NextEventHandler<T> : EventHandler<T>
{
    public NextEventHandler(Action<T> action) : base(action)
    {
    }

    public override bool ShouldUnsubscribe() => true;
}

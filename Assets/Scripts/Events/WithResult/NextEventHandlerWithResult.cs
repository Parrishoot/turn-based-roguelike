using System;
using UnityEngine;

public class NextEventHandlerWithResult<T> : EventHandlerWithResult<T>
{
    public NextEventHandlerWithResult(Action<T> action) : base(action)
    {
    }

    public override bool ShouldUnsubscribe() => true;
}

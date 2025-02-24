using System;
using UnityEngine;

public abstract class EventHandlerWithResult<T> : IEventHandlerWithResult<T>
{
    protected Action<T> OnEvent;

    public EventHandlerWithResult(Action<T> action) {
        OnEvent = action;
    }

    public void Handle(T result)
    {
        OnEvent?.Invoke(result);
    }

    public abstract bool ShouldUnsubscribe();
}

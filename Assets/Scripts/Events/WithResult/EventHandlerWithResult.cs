using System;
using UnityEngine;

public abstract class EventHandler<T> : IEventHandler<T>
{
    protected Action<T> OnEvent;

    public EventHandler(Action<T> action) {
        OnEvent = action;
    }

    public void Handle(T result)
    {
        OnEvent?.Invoke(result);
    }

    public abstract bool ShouldUnsubscribe();
}

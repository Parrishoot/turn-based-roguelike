using System;
using UnityEngine;

public abstract class EventHandler : IEventHandler
{
    protected Action OnEvent;

    public EventHandler(Action action) {
        OnEvent = action;
    }

    public void Handle()
    {
        OnEvent?.Invoke();
    }

    public abstract bool ShouldUnsubscribe();
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventProcessor
{
    private List<IEventHandler> handlers = new List<IEventHandler>();

    public Action OnEvent { get; set; }

    public void Process() {

        OnEvent?.Invoke();

        IEventHandler[] handlersToUnsubscribe = handlers.Where(x => x.ShouldUnsubscribe()).ToArray();
        foreach(IEventHandler handler in handlersToUnsubscribe) {
            UnsubscribeHandler(handler);
        }
    }

    public void OnNext(Action onNextAction) {
        SubscribeHandler(new NextEventHandler(onNextAction));
    }

    public void OnEvery(Action onNextAction) {
        SubscribeHandler(new EveryEventHandler(onNextAction));
    }

    public void SubscribeUntil(Action onNextAction, Func<bool> predicate) {
        SubscribeHandler(new EventUntilHandler(onNextAction, predicate));
    }

    private void SubscribeHandler(IEventHandler handler)
    {
        OnEvent += handler.Handle;
        handlers.Add(handler);
    }

    private void UnsubscribeHandler(IEventHandler handler)
    {
        OnEvent -= handler.Handle;

        if(handlers.Contains(handler)) {
            handlers.Remove(handler);
        }
    }
}

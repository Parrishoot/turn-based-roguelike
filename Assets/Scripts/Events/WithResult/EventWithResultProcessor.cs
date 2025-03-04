using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventProcessor<T>
{
    private List<IEventHandler<T>> handlers = new List<IEventHandler<T>>();

    public Action<T> OnEvent { get; set; }

    public void Process(T result) {

        OnEvent?.Invoke(result);

        IEventHandler<T>[] handlersToUnsubscribe = handlers.Where(x => x.ShouldUnsubscribe()).ToArray();
        foreach(IEventHandler<T> handler in handlersToUnsubscribe) {
            UnsubscribeHandler(handler);
        }
    }

    public void OnNext(Action<T> onNextAction) {
        SubscribeHandler(new NextEventHandler<T>(onNextAction));
    }

    public void OnEvery(Action<T> onNextAction) {
        SubscribeHandler(new EveryEventHandler<T>(onNextAction));
    }

    public void SubscribeUntil(Action<T> onNextAction, Func<bool> predicate) {
        SubscribeHandler(new EventUntilHandler<T>(onNextAction, predicate));
    }

    public void SubscribeHandler(IEventHandler<T> handler)
    {
        OnEvent += handler.Handle;
        handlers.Add(handler);
    }

    public void UnsubscribeHandler(IEventHandler<T> handler)
    {
        OnEvent -= handler.Handle;

        if(handlers.Contains(handler)) {
            handlers.Remove(handler);
        }
    }
}

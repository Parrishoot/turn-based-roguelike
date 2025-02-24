using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventWithResultProcessor<T>
{
    private List<IEventHandlerWithResult<T>> handlers = new List<IEventHandlerWithResult<T>>();

    public Action<T> OnEvent { get; set; }

    public void Process(T result) {

        OnEvent?.Invoke(result);

        IEventHandlerWithResult<T>[] handlersToUnsubscribe = handlers.Where(x => x.ShouldUnsubscribe()).ToArray();
        foreach(IEventHandlerWithResult<T> handler in handlersToUnsubscribe) {
            UnsubscribeHandler(handler);
        }
    }

    public void OnNext(Action<T> onNextAction) {
        SubscribeHandler(new NextEventHandlerWithResult<T>(onNextAction));
    }

    public void OnEvery(Action<T> onNextAction) {
        SubscribeHandler(new EveryEventHandlerWithResult<T>(onNextAction));
    }

    public void SubscribeUntil(Action<T> onNextAction, Func<bool> predicate) {
        SubscribeHandler(new EventUntilHandlerWithResult<T>(onNextAction, predicate));
    }

    private void SubscribeHandler(IEventHandlerWithResult<T> handler)
    {
        OnEvent += handler.Handle;
        handlers.Add(handler);
    }

    private void UnsubscribeHandler(IEventHandlerWithResult<T> handler)
    {
        OnEvent -= handler.Handle;

        if(handlers.Contains(handler)) {
            handlers.Remove(handler);
        }
    }
}

using System;
public class EveryEventHandlerWithResult<T> : EventHandlerWithResult<T>
{
    public EveryEventHandlerWithResult(Action<T> action) : base(action)
    {
    }

    public override bool ShouldUnsubscribe() => false;
}

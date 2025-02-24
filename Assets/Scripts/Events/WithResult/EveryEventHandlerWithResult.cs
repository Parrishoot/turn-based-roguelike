using System;
public class EveryEventHandler<T> : EventHandler<T>
{
    public EveryEventHandler(Action<T> action) : base(action)
    {
    }

    public override bool ShouldUnsubscribe() => false;
}

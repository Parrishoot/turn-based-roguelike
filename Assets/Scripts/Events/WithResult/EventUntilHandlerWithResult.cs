using System;

public class EventUntilHandler<T> : EventHandler<T>
{
    private Func<bool> predicate;

    public EventUntilHandler(Action<T> action, Func<bool> predicate) : base(action)
    {
        this.predicate = predicate;
    }

    public override bool ShouldUnsubscribe()
    {
        return predicate.Invoke();
    }
}

using System;

public class EventUntilHandlerWithResult<T> : EventHandlerWithResult<T>
{
    private Func<bool> predicate;

    public EventUntilHandlerWithResult(Action<T> action, Func<bool> predicate) : base(action)
    {
        this.predicate = predicate;
    }

    public override bool ShouldUnsubscribe()
    {
        return predicate.Invoke();
    }
}

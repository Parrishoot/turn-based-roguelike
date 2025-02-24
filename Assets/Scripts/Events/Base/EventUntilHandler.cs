using System;

public class EventUntilHandler : EventHandler
{
    private Func<bool> predicate;

    public EventUntilHandler(Action action, Func<bool> predicate) : base(action)
    {
        this.predicate = predicate;
    }

    public override bool ShouldUnsubscribe()
    {
        return predicate.Invoke();
    }
}

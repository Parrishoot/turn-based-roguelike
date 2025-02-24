using UnityEngine;

public class NextEventHandler : EventHandler
{
    public NextEventHandler(System.Action action) : base(action)
    {
    }

    public override bool ShouldUnsubscribe() => true;
}

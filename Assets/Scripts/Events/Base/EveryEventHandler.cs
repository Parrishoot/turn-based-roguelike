using UnityEngine;

public class EveryEventHandler : EventHandler
{
    public EveryEventHandler(System.Action action) : base(action)
    {
    }

    public override bool ShouldUnsubscribe() => false;
}

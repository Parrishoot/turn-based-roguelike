using UnityEngine;

public interface IEventHandler
{
    void Handle();
    bool ShouldUnsubscribe();
}

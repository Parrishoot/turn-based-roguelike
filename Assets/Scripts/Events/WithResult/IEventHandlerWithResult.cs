using UnityEngine;

public interface IEventHandler<T>
{
    void Handle(T result);

    bool ShouldUnsubscribe();
}

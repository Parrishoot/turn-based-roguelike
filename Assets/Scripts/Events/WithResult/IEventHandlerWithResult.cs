using UnityEngine;

public interface IEventHandlerWithResult<T>
{
    void Handle(T result);

    bool ShouldUnsubscribe();
}

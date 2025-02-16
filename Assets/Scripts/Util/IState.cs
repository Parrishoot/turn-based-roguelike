using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnStart();

    void OnUpdate(float deltaTime);

    void OnFixedUpdate(float fixedDeltaTime);

    void OnEnd();
}

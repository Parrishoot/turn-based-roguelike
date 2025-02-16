using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericState<T>: IState
where T: StateMachine
{
    protected T StateMachine { get; set; }

    public GenericState(T stateMachine) {
        this.StateMachine = stateMachine;
    }

    public abstract void OnStart();

    public abstract void OnUpdate(float deltaTime);

    public virtual void OnFixedUpdate(float fixedDeltaTime) {

    }

    public abstract void OnEnd();
}

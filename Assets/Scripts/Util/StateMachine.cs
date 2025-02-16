using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected IState currentState { get; private set;}

    // Update is called once per frame
    protected virtual void Update()
    {
        currentState?.OnUpdate(Time.deltaTime);
    }

    protected void FixedUpdate() {
        currentState?.OnFixedUpdate(Time.fixedDeltaTime);    
    }

    public void ChangeState(IState newState) {
        currentState?.OnEnd();

        currentState = newState;
        newState.OnStart();
    }
}
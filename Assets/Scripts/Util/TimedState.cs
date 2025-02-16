using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimedState: State
{
    private float time;

    private Timer currentTimer;

    public TimedState(StateMachine stateMachine, float time): base(stateMachine) {
        this.time = time;
    }

    public override void OnStart()
    {
        currentTimer = new Timer(time);
        currentTimer.AddOnTimerFinishedEvent(() => {
            OnTimerEnd();
        }); 
    }

    public override void OnUpdate(float deltaTime)
    {
        currentTimer?.DecreaseTime(deltaTime);
    }

    protected abstract void OnTimerEnd();
}
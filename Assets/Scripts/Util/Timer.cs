using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer 
{
    private float timeRemaining = 0f;

    private float totalTime = 0f;

    public delegate void TimerFinishedEvent();

    private TimerFinishedEvent onTimerFinishedEvent;
 
    public Timer(float timeRemaining) {
        this.timeRemaining = timeRemaining;
        totalTime = timeRemaining;
    }

    public bool IsFinished() {
        return timeRemaining <= 0f;
    }

    public void DecreaseTime(float deltaTime) {

        if(IsFinished()) {
            return;
        }

        timeRemaining = Mathf.Max(timeRemaining - deltaTime, 0f);

        if(IsFinished()) {
            onTimerFinishedEvent?.Invoke();
        }
    }

    public float GetTimeRemaining() {
        return timeRemaining;
    }

    public float GetPercentageRemaining() {
        return timeRemaining / totalTime;;
    }

    public float GetPercentageFinished() {
        return 1 - GetPercentageRemaining();
    }

    public void AddOnTimerFinishedEvent(TimerFinishedEvent timerFinishedEvent) {
        onTimerFinishedEvent += timerFinishedEvent;
    }
}
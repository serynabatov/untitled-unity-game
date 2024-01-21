using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Action TimerAction;
    private float timer;
    private bool armed;

    public void SetTimer(float timer, Action TimerAction)
    {
        this.armed = true;
        this.timer = timer;
        this.TimerAction = TimerAction;
    }

    public void ResetTimer()
    {
        this.armed = false;
        this.timer = 0f;
        this.TimerAction = null;
    }

    private void Update()
    {
        if ((timer > 0f) && armed)
        {
            timer -= Time.deltaTime;
        }
        if (TimerComplete())
        {
            TimerAction?.Invoke();
            TimerAction = null;
        }
    }

    private bool TimerComplete()
    {
        return timer <= 0f;
    }
}

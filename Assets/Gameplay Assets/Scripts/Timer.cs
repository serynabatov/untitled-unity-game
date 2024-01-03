using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private Action TimerAction;
    private float timer;

    public void SetTimer(float timer, Action TimerAction)
    {
        this.timer = timer;
        this.TimerAction = TimerAction;
    }

    private void Update()
    {
        if (timer > 0f)
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Timer.
/// </summary>
public class Timer : MonoBehaviour
{

    #region Fields

    // timer duration
    float totalSeconds = 0;

    // timer execution
    float elapsedSeconds = 0;

    // check if the timer is running
    bool running = false;

    // support for countdown seconds values
    int previousCountdownValue;

    // support for Finished property
    bool started = false;

    // events invoked by class
    TimerChangedEvent timerChangedEvent = new TimerChangedEvent();
    TimerFinishedEvent timerFinishedEvent = new TimerFinishedEvent();

    #endregion

    #region Properties

    /// <summary>
    /// Sets the duration of the timer
    /// The duration can only set if the timer is not running
    /// </summary>
    /// <value>The duration.</value>
    public float Duration
    {
        set
        {
            if(!running)
            {
                totalSeconds = value;
            }
        }
    }

    /// <summary>
    /// Returns false if the timer has never been started or still running
    /// Returns true if the timer has finished
    /// </summary>
    /// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
    public bool Finished
    {
        get
        {
            return started && !running;
        }
    }

    /// <summary>
    /// Gets whether or not timer is running currently
    /// </summary>
    /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
    public bool Running
    {
        get
        {
            return running;
        }
    }

    /// <summary>
    /// Gets the timer changed event.
    /// Consumers could reference to this object and not create their own object!
    /// </summary>
    /// <value>The timer changed event.</value>
    public TimerChangedEvent TimerChangedEvent
    {
        get
        {
            return timerChangedEvent;
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Update this instance.
    /// </summary>
    public void Update()
    {
        // update timer
        if(running)
        {
            elapsedSeconds += Time.deltaTime;

            // check for new countdown value
            int newCountdownValue = GetCurrentCountdownValue();
            if (newCountdownValue != previousCountdownValue)
            {
                previousCountdownValue = newCountdownValue;
                timerChangedEvent.Invoke(previousCountdownValue);
            }

            // check for timer finished
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
                timerFinishedEvent.Invoke();
            }

        }
    }

    /// <summary>
    /// Run this instance.
    /// check if the total seconds is larger than 0
    /// because timer you cannot instantiate a timer with the negative
    /// number of seconds to run
    /// </summary>
    public void Run()
    {
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            elapsedSeconds = 0;

            // calculate initial countdown and an event
            previousCountdownValue = GetCurrentCountdownValue();
            timerChangedEvent.Invoke(previousCountdownValue);
        }
    }

    /// <summary>
    /// Stop this instance.
    /// </summary>
    public void Stop()
    {
        started = false;
        running = false;
    }

    /// <summary>
    /// Adds the timer changed event listener.
    /// </summary>
    /// <param name="handler">Handler.</param>
    public void AddTimerChangedEventListener(UnityAction<int> handler)
    {
        timerChangedEvent.AddListener(handler);
    }

    /// <summary>
    /// Adds the timer finished event listener.
    /// </summary>
    /// <returns>The timer finished event listener.</returns>
    /// <param name="handler">Handler.</param>
    public void AddTimerFinishedEventListener(UnityAction handler)
    {
        timerFinishedEvent.AddListener(handler);
    }
    #endregion

    #region Private methods

    private int GetCurrentCountdownValue()
    {
        return (int)Mathf.Ceil(totalSeconds - elapsedSeconds);
    }   

    #endregion
}

using System;
using UnityEngine;

public class Timer
{
    public event Action<float> TimerValueChangedEvent;
    public event Action TimerFinishedEvent;

    public TimerType type { get; }
    public float RemainingSeconds { get; private set; }
    public bool IsPaused { get; private set; }


    public Timer(TimerType type)
    {
        this.type = type;
    }

    public Timer(TimerType type, float seconds)
    {
        this.type = type;
        SetTime(seconds);
    }

    public void SetTime(float seconds)
    {
        RemainingSeconds = seconds;
        TimerValueChangedEvent?.Invoke(RemainingSeconds);
    }

    public void Start()
    {
        if (RemainingSeconds == 0)
        {
            Debug.LogError("TIMER: You are trying start timer with remaining seconds equal 0 ");
            TimerFinishedEvent?.Invoke();
        }

        IsPaused = false;
        Subscribe();
        TimerValueChangedEvent?.Invoke(RemainingSeconds);
    }

    public void Start(float seconds)
    {
        SetTime(seconds);
        Start();
    }

    public void Pause()
    {
        IsPaused = true;
        UnSubscribe();
        TimerValueChangedEvent?.Invoke(RemainingSeconds);
    }

    public void Unpause()
    {
        IsPaused = false;
        Subscribe();
        TimerValueChangedEvent?.Invoke(RemainingSeconds);
    }

    public void Stop()
    {
        UnSubscribe();
        RemainingSeconds = 0;

        TimerValueChangedEvent?.Invoke(RemainingSeconds);
        TimerFinishedEvent?.Invoke();
    }

    private void Subscribe()
    {
        switch (type)
        {
            case TimerType.UpdateTick:
                TimeInvoker.Instance.UpdateTimeTickedEvent += OnUpdateTick;
                break;
            case TimerType.UpdateTickUnscaled:
                TimeInvoker.Instance.UpdateTimeUnscaledTickedEvent += OnUpdateTick;
                break;
            case TimerType.OneSecTick:
                TimeInvoker.Instance.OneSecondTickedEvent += OnOneSecondtick;
                break;
            case TimerType.OneSecTickUnscaled:
                TimeInvoker.Instance.OneSecondUnscaledTickedEvent += OnOneSecondtick;
                break;
            default:
                break;
        }


    }

    private void UnSubscribe()
    {

        switch (type)
        {
            case TimerType.UpdateTick:
                TimeInvoker.Instance.UpdateTimeTickedEvent -= OnUpdateTick;
                break;
            case TimerType.UpdateTickUnscaled:
                TimeInvoker.Instance.UpdateTimeUnscaledTickedEvent -= OnUpdateTick;
                break;
            case TimerType.OneSecTick:
                TimeInvoker.Instance.OneSecondTickedEvent -= OnOneSecondtick;
                break;
            case TimerType.OneSecTickUnscaled:
                TimeInvoker.Instance.OneSecondUnscaledTickedEvent -= OnOneSecondtick;
                break;
            default:
                break;
        }
    }

    private void OnOneSecondtick()
    {
        if (IsPaused)
            return;

        RemainingSeconds -= 1f;
        CheckFinish();
    }

    private void OnUpdateTick(float deltaTime)
    {
        if (IsPaused)
            return;

        RemainingSeconds -= deltaTime;
        CheckFinish();
    }

    private void CheckFinish()
    {
        if (RemainingSeconds <= 0)
        {
            Stop();
        }
        else
        {
            TimerValueChangedEvent?.Invoke(RemainingSeconds);
        }
    }

}
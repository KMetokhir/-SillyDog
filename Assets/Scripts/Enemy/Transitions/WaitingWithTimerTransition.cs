using UnityEngine;

public class WaitingWithTimerTransition : EnemyTransition
{
    [SerializeField] private TimerType _type;
    [SerializeField] private float _timerSeconds;

    private Timer _timer;


    private void OnValidate()
    {
        _timerSeconds = Mathf.Clamp(_timerSeconds, 0, int.MaxValue);
    }

    protected override void Enable()
    {
        _timer = new Timer(_type, _timerSeconds);
        _timer.TimerFinishedEvent += OnTimerFinished;
        _timer.Start();

    }

    private void OnTimerFinished()
    {
        NeedTransit = true;
    }

    private void OnDisable()
    {
        _timer.TimerFinishedEvent -= OnTimerFinished;
    }


}

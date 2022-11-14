using UnityEngine;

public class AngryToChaseTransition : EnemyTransition
{

    [SerializeField] private TimerType _type;
    [SerializeField] private float _timerSeconds;

    private Timer _timer;


    private void OnValidate()
    {
        _timerSeconds = Mathf.Clamp(_timerSeconds, 0, float.MaxValue);
    }

    protected override void Enable()
    {
        CollisionTracker.PeeAriaExitEvent += OnPeeAriaExit;
        _timer = new Timer(_type, _timerSeconds);
        _timer.Pause();

    }

    private void OnDisable()
    {
        _timer.TimerFinishedEvent -= OnTimerFinished;
        CollisionTracker.PeeAriaExitEvent -= OnPeeAriaExit;
    }

    private void OnTimerFinished()
    {
        NeedTransit = true;
    }

    private void OnPeeAriaExit(PeeAria peeAria)
    {

        if (PeeAria == peeAria)
        {            
            PeeAria.gameObject.SetActive(false);
            _timer.Unpause();
            _timer.TimerFinishedEvent += OnTimerFinished;

        }
    }

}

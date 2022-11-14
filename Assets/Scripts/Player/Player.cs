using System;
using UnityEngine;

[RequireComponent(typeof(CollisionTracker))]
public class Player : MonoBehaviour
{
    public event Action DamagedEvent;
    public event Action DeadEvent;
    public event Action PlayerWasAttackedEvent;

    [SerializeField] private ParticleSystem _peeStream;    

    [SerializeField] private TimerType _type;
    [SerializeField] private float _timerSeconds;

    private CollisionTracker _collisionTracker;
    private Bladder _bladder;
    private PeeAria _currentPeeAria;

    private Timer _deathTimer;
    private DeathTimerView _timerView;


    private void OnValidate()
    {
        _timerSeconds = Mathf.Clamp(_timerSeconds, 0, float.MaxValue);
    }

    private void Awake()
    {
        _bladder = GetComponentInChildren<Bladder>();
        _collisionTracker = GetComponent<CollisionTracker>();

        _deathTimer = new Timer(_type, _timerSeconds);
        _timerView = GetComponentInChildren<DeathTimerView>();

    }

    private void OnEnable()
    {
        _collisionTracker.PeeAriaEnteredEvent += OnPeeAriaEntered;
        _collisionTracker.PeeAriaExitEvent += OnPeeAriaExitEvent;
        _bladder.BladderEmptyEvent += OnBladderEmpty;
        _bladder.BladderIsFullEvent += OnBladderIsFull;
        _deathTimer.TimerFinishedEvent += OnTimerFinished;
    }

    private void OnDisable()
    {
        _collisionTracker.PeeAriaEnteredEvent -= OnPeeAriaEntered;
        _collisionTracker.PeeAriaExitEvent -= OnPeeAriaExitEvent;
        _bladder.BladderEmptyEvent -= OnBladderEmpty;
        _deathTimer.TimerFinishedEvent -= OnTimerFinished;
        _deathTimer.TimerValueChangedEvent -= OnTimerChanged;
    }

    private void OnBladderIsFull()
    {
        _timerView.IsHide(false);
        _deathTimer.TimerValueChangedEvent += OnTimerChanged;
        _deathTimer.SetTime(_timerSeconds);
        _deathTimer.Start();
    }

    private void OnTimerChanged(float value)
    {
        _timerView.SetTime(value);
    }

    private void OnTimerFinished()
    {
        DeadEvent?.Invoke();
    }

    private void OnBladderEmpty()
    {
        if (_currentPeeAria != null)
        {
            _currentPeeAria.PourOut();
            _peeStream.Stop();
            _bladder.Fill();
        }
    }

    private void OnPeeAriaExitEvent(PeeAria peeAria)
    {
        _currentPeeAria.PeeAriaIsFullEvent -= OnPeeAriaFull;
        peeAria.PourOut();
        _bladder.Fill();
        _peeStream.Stop();
    }

    private void OnPeeAriaEntered(PeeAria peeAria)
    {
        _currentPeeAria = peeAria;
        _currentPeeAria.PeeAriaIsFullEvent += OnPeeAriaFull;
        _currentPeeAria.Fill(_bladder.PeeVelocity);
        _bladder.Pee(_currentPeeAria.FillVelocityBonus);


        var dogRelativePeeAria = this.transform.InverseTransformPoint(peeAria.transform.position);

        if (dogRelativePeeAria.x > 0)
        {
            _peeStream.transform.localEulerAngles = new Vector3(20, 90, -90);
        }
        else
        {
            _peeStream.transform.localEulerAngles = new Vector3(160, 90, -90);
        }

        _peeStream.Play();

        if (_deathTimer.IsPaused == false)
        {
            _deathTimer.Pause();
            _timerView.IsHide(true);

        }

    }

    private void OnPeeAriaFull()
    {
        _bladder.Fill();
        _peeStream.Stop();
    }

    public void TakeDamage(int value)
    {
        DamagedEvent?.Invoke();
        _bladder.IncreaseLquideLevel(value);
    }

    public void TakeAttack()
    {
        PlayerWasAttackedEvent?.Invoke();
    }
}

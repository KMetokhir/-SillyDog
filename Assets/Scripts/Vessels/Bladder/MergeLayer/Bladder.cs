using System;
using UnityEngine;

public class Bladder : VesselOwner
{
    public event Action BladderEmptyEvent;
    public event Action BladderIsFullEvent;
    public float PeeVelocity => _peeVelocity;

    [SerializeField] private float _fillVelocity;
    [SerializeField] private float _peeVelocity;

    private BladderModel _model;
    private BladderView _view;


    public override void Validate()
    {
        _fillVelocity = Mathf.Clamp(_fillVelocity, 0, float.MaxValue);
        _peeVelocity = Mathf.Clamp(_peeVelocity, 0, float.MaxValue);
    }

    private void Awake()
    {
        _model = BladderModel.GetInstance(this, _fillVelocity, _peeVelocity);
        _view = GetComponent<BladderView>();
    }

    private void OnEnable()
    {
        _model.LiquideLevelChangedEvent += OnPeeLevelChanged;
        _model.VesselIsEmptyEvent += OnBladderIsEmpty;
        _model.VesselIsFullEvent += OnBladderFull;
    }

    private void Start()
    {
        _model.FillBladder();
        IncreaseLquideLevel(0);
    }

    public override void Disable()
    {
        _model.LiquideLevelChangedEvent -= OnPeeLevelChanged;
        _model.VesselIsEmptyEvent -= OnBladderIsEmpty;
    }

    public void Fill()
    {
        _model.FillBladder();
    }

    public void Pee(float velocityBonus)
    {
        _model.PourOutBladder(velocityBonus);
    }

    public void IncreaseLquideLevel(int value)
    {
        _model.IncreaseLiquideLevel(value);
    }

    private void OnBladderFull()
    {
        BladderIsFullEvent?.Invoke();
    }

    private void OnBladderIsEmpty()
    {
        BladderEmptyEvent?.Invoke();
    }

    private void OnPeeLevelChanged(int peeLevel, float velocity)
    {
        _view.SetPeeLevel(peeLevel, LiquidMaxLevel, velocity);
    }
}

using System;
using UnityEngine;


public class PeeAria : VesselOwner
{
    public event Action PeeAriaIsFullEvent;

    public float FillVelocityBonus => _fillVelocityBonus;

    [SerializeField] private float _fillVelocityBonus;   
    [SerializeField] private float _pourOutVelocity;

    private PeeAriaView _view;
    private PeeAriaModel _model;

    public override void Validate()
    {
        _fillVelocityBonus = Mathf.Clamp(_fillVelocityBonus, 0, float.MaxValue);
        _pourOutVelocity = Mathf.Clamp(_pourOutVelocity, 0, float.MaxValue);
    }

    private void Awake()
    {
        _model = new PeeAriaModel(this,_pourOutVelocity, _fillVelocityBonus);
        _view = GetComponent<PeeAriaView>();
    }

    private void OnEnable()
    {      
        _model.LiquideLevelChangedEvent += OnPeeLevelChanged;
        _model.VesselIsFullEvent += OnPeeAriaFull;
    }

    private void Start()
    {
        _model.PourOutPeeAria();
    }

    public override void Disable()
    {
        _model.LiquideLevelChangedEvent -= OnPeeLevelChanged;
        _model.VesselIsFullEvent -= OnPeeAriaFull;
    }

    public void PourOut()
    {
        _model.PourOutPeeAria();
    }

    public void Fill(float fillVelocity)
    {       
        _model.FillPeeAria(fillVelocity);
    }    

    private void OnPeeAriaFull()
    {        
        PeeAriaIsFullEvent?.Invoke();
    }

    private void OnPeeLevelChanged(int peeLevel, float velocity)
    {
        _view.SetPeeLevel(peeLevel, LiquidMaxLevel, velocity);
    }
    
}

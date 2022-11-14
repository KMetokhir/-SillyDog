using System;
using System.Collections;
using UnityEngine;

public abstract class VesselModel
{
    public event Action VesselIsEmptyEvent;
    public event Action<int, float> LiquideLevelChangedEvent;
    public event Action VesselIsFullEvent;

    private int _liquidLevel;
    private int _liquidMaxLevel;
    private float _velocityInIncreaseMethod = 0.01f;

    private Coroutine _coroutine;
    private VesselOwner _owner;

    private bool _isFiling;
    private float _currentVelocity;


    public VesselModel(VesselOwner owner)
    {
        _owner = owner;
        _liquidMaxLevel = owner.LiquidMaxLevel;
        _liquidLevel = owner.LiquidLevel;
    }

    public void IncreaseLiquideLevel(int value)
    {
        value = Mathf.Clamp(value, 0, int.MaxValue);

        if (_liquidLevel == _liquidMaxLevel)
        {
            return;
        }
        _owner.StopCoroutine(_coroutine);
        _liquidLevel += value;

        if (_liquidLevel >= _liquidMaxLevel)
        {
            _liquidLevel = _liquidMaxLevel;
        }

        LiquideLevelChangedEvent?.Invoke(_liquidLevel, _velocityInIncreaseMethod);

        if (_isFiling)
        {
            Fill(_currentVelocity);
        }
        else
        {
            PourOut( _currentVelocity);
        }
    }

    protected void Fill( float fillVelocity) 
    {       
        if (_coroutine != null)
        {
            _owner.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _isFiling = true;       
       
        _currentVelocity = fillVelocity;
        _coroutine = _owner.StartCoroutine(FillCoroutin(fillVelocity));
    }

    protected void PourOut(float pourOutVelocity)
    {
        if (_coroutine != null)
        {
            _owner.StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _isFiling = false;

        _currentVelocity = pourOutVelocity;
        LiquideLevelChangedEvent?.Invoke(_liquidLevel, pourOutVelocity);  // remove - for tests      
        _coroutine = _owner.StartCoroutine(PourOutCoroutine(pourOutVelocity));
    }

    private IEnumerator FillCoroutin(float fillVelocity)
    {
        while (_liquidLevel < _liquidMaxLevel)
        {
            _liquidLevel += 1;
            LiquideLevelChangedEvent?.Invoke(_liquidLevel, fillVelocity);
            yield return new WaitForSeconds(fillVelocity);
        }

        VesselIsFullEvent?.Invoke();
    }

    private IEnumerator PourOutCoroutine(float pourOutVelocity)
    {

        while (_liquidLevel > 0)
        {
            _liquidLevel -= 1;            
            LiquideLevelChangedEvent?.Invoke(_liquidLevel, pourOutVelocity);
            yield return new WaitForSeconds(pourOutVelocity);
        }

        VesselIsEmptyEvent?.Invoke();
    }
}

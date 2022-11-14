using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract  class VesselOwner : MonoBehaviour
{
    [SerializeField] private int _liquidLevel;
    [SerializeField] private int _liquidMaxLevel;

    public int LiquidLevel => _liquidLevel;
    public int LiquidMaxLevel => _liquidMaxLevel;

    private void OnValidate()
    {
        _liquidMaxLevel = Mathf.Clamp(_liquidMaxLevel, 0, int.MaxValue);
        _liquidLevel = Mathf.Clamp(_liquidLevel, 0, int.MaxValue);
        Validate();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        Disable();
    }

    public abstract void Disable();
    public abstract void Validate();
}

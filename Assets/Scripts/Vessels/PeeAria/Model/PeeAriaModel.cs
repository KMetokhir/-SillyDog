using UnityEngine;

public class PeeAriaModel : VesselModel
{
    private float _pourOutVelocity;
    private float _fillVelocityBonus;

    public PeeAriaModel(VesselOwner owner, float pourOutVelocity, float velocityBonus = 0) : base(owner)
    {
        _pourOutVelocity = pourOutVelocity;
        _fillVelocityBonus = velocityBonus;

        _pourOutVelocity = Mathf.Clamp(_pourOutVelocity, 0, float.MaxValue);
        _fillVelocityBonus = Mathf.Clamp(_fillVelocityBonus, 0, float.MaxValue);
    }

    public void FillPeeAria( float fillVelocity)
    {
        Fill(fillVelocity - _fillVelocityBonus);
    }

    public void PourOutPeeAria()
    {
        PourOut( _pourOutVelocity);
    }

}

using UnityEngine;

public class BladderModel : VesselModel
{
    private static BladderModel _instance;
    private float _fillVelocity;
    private float _pourOutVelocity;

    private BladderModel(VesselOwner owner, float fillVelocity, float pourOutVelocity) : base(owner)
    {
        _pourOutVelocity = pourOutVelocity;
        _fillVelocity = fillVelocity;

        _pourOutVelocity = Mathf.Clamp(_pourOutVelocity, 0, float.MaxValue);
        _fillVelocity = Mathf.Clamp(_fillVelocity, 0, float.MaxValue);
    }

    public static BladderModel GetInstance(VesselOwner owner, float fillVelocity, float pourOutVelocity)
    {
        if (_instance == null)
        {
            _instance = new BladderModel(owner, fillVelocity, pourOutVelocity);
        }
        return _instance;
    }

    public void FillBladder()
    {
        Fill(_fillVelocity);
    }

    public void PourOutBladder(float velocityBonus)
    {
        if (velocityBonus > _pourOutVelocity)
        {
            Debug.LogError($"velocity bonus {velocityBonus} is bigger then pourOut vilocity {_pourOutVelocity}");
        }
        PourOut(_pourOutVelocity - velocityBonus);
    }   
    
}


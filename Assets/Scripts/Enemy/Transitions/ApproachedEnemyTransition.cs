using UnityEngine;

public class ApproachedEnemyTransition : EnemyTransition
{
    [SerializeField] private float _approachedEnemyDistance;


    private void OnValidate()
    {
        _approachedEnemyDistance = Mathf.Clamp(_approachedEnemyDistance, 0, float.MaxValue);
    }

    protected override void Enable()
    {

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < _approachedEnemyDistance)
        {
            NeedTransit = true;
        }
    }

}

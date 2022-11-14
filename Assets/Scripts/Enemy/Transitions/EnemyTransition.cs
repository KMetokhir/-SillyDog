using UnityEngine;

public abstract class EnemyTransition : MonoBehaviour
{
    public EnemyState TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    protected Player Player { get; private set; }
    protected PeeAria PeeAria { get; private set; }    
    protected CollisionTracker CollisionTracker { get; private set; }

    [SerializeField] private EnemyState _targetState;


    public void Init(Player player, PeeAria peearia, CollisionTracker collisionTracker)
    {
        Player = player;
        PeeAria = peearia;
        CollisionTracker = collisionTracker;
        
    }

    private void OnEnable()
    {
        Enable();
        NeedTransit = false;
    }

    protected abstract void Enable();



}

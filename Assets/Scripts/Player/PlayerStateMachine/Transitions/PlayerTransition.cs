using UnityEngine;

public abstract class PlayerTransition : MonoBehaviour
{
    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    protected Player Player {  get; private set; }
    protected PlayerInput Input { get; private set; }
    protected Animator Animator { get; private set; }

    [SerializeField] private State _targetState;


    private void OnEnable()
    {
        NeedTransit = false;
        Enable();
    }

    public abstract void Enable();

    public void Init(Player player, PlayerInput input, Animator animator)
    {
        Player = player;
        Input = input;
        Animator = animator;
    }
    
}

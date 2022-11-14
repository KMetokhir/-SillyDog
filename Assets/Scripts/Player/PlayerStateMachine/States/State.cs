using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private PlayerTransition[] _transitions;

    protected Rigidbody Rigidbody { get; private set; }
    protected Animator Animator { get; private set; }
    protected PlayerInput Input { get; private set; }
    protected Player Player { get; private set; }



    public void Enter(Rigidbody rigidbody, Animator animator, PlayerInput input, Player player)
    {
        if (enabled == false)
        {
            Rigidbody = rigidbody;
            Animator = animator;
            Input = input;
            Player = player;
            enabled = true;

            foreach (var transition in _transitions)
            {
                transition.Init(Player, Input, Animator);
                transition.enabled = true;
            }
        }
    }

    public void Exit()
    {

        foreach (var transition in _transitions)
        {
            transition.enabled = false;
        }

        enabled = false;
    }

    public State GetNextState()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}

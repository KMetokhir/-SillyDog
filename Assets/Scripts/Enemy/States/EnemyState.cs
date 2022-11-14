using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected Rigidbody Rigidbody { get; private set; }
    protected Animator Animator { get; private set; }
    protected Player Player { get; private set; }
    protected PeeAria PeeAria { get; private set; }

    [SerializeField] private EnemyTransition[] _transitions;

    private CollisionTracker _collisionTracker;

    public void Enter(Rigidbody rigidbody, Animator animator, Player player, PeeAria peeAria, CollisionTracker collisionTracker)
    {
        if (enabled == false)
        {
            Rigidbody = rigidbody;
            Animator = animator;
            Player = player;           
            PeeAria = peeAria;
            _collisionTracker = collisionTracker;

            enabled = true;

            foreach (var transition in _transitions)
            {          
                transition.Init(Player, PeeAria, _collisionTracker);
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


    public EnemyState GetNextState()
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

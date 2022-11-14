using System;
using UnityEngine;

public class BrokenAnimationBehaviour : StateMachineBehaviour
{
    public event Action BrokenAnimationEndedEvent;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        BrokenAnimationEndedEvent?.Invoke();
    }
}

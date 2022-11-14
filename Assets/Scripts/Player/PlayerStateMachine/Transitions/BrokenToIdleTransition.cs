using UnityEngine;

public class BrokenToIdleTransition : PlayerTransition
{
    private BrokenAnimationBehaviour brokenAnimation;   

    public override void Enable()
    {
        brokenAnimation = Animator.GetBehaviour<BrokenAnimationBehaviour>();
        brokenAnimation.BrokenAnimationEndedEvent += OnBrokenAnimationEnded;
    }

    private void OnBrokenAnimationEnded()
    {
        NeedTransit = true;
    }

    private void OnDisable()
    {
        brokenAnimation.BrokenAnimationEndedEvent -= OnBrokenAnimationEnded;
    }

}

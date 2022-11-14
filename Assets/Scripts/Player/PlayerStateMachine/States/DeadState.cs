using UnityEngine;

public class DeadState : State
{
    private void OnEnable()
    {
        Rigidbody.velocity = Vector3.zero;
        Animator.SetTrigger("IsDeadT");
    }
}

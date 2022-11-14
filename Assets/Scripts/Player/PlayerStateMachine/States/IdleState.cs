using UnityEngine;

public class IdleState : State
{
    private void OnEnable()
    {
        Rigidbody.velocity = Vector3.zero;
    }
}

using UnityEngine;

public class BrokenState : State
{
    private void OnEnable()
    {
        Player.DamagedEvent += OnDamaged;
        Rigidbody.velocity = Vector3.zero;
    }

    private void OnDisable()
    {
        Player.DamagedEvent -= OnDamaged;
        Animator.SetBool("IsBroken", false);
    }

    private void OnDamaged()
    {
        Animator.SetBool("IsBroken", true);
    }

    
}

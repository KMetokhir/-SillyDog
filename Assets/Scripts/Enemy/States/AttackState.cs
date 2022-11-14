using DG.Tweening;
using UnityEngine;

public class AttackState : EnemyState
{
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _jumpDuration;   
    [SerializeField] private int _atackValue;

    private Tween _jumpTween;


    private void OnValidate()
    {
        _jumpPower = Mathf.Clamp(_jumpPower, 0, float.MaxValue);
        _jumpDuration = Mathf.Clamp(_jumpDuration, 0, float.MaxValue);
        _atackValue = Mathf.Clamp(_atackValue, 0, int.MaxValue);
    }

    private void OnEnable()
    {
        Animator.SetBool("Attack", true);
        _jumpTween = transform.DOJump(Player.transform.position, _jumpPower, 1, _jumpDuration).OnStart
            (() => Player.TakeAttack()).OnComplete(() => Player.TakeDamage(_atackValue));
    }

    private void OnDisable()
    {
        Animator.SetBool("Attack", false);
        _jumpTween.Kill();
    }


}

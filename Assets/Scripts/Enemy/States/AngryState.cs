using DG.Tweening;
using UnityEngine;

public class AngryState : EnemyState
{
    [SerializeField] private float _rotationDuration;

    private Collider _collider;
    private Tween tween;
    private Vector3 PlayerPosition;


    private void OnValidate()
    {
        _rotationDuration = Mathf.Clamp(_rotationDuration, 0, float.MaxValue);
    }

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnEnable()
    {        
        Animator.SetBool("IsAngry", true);       
        PlayerPosition = Player.transform.position;
        PeeAria.transform.parent = null;
        tween = Rigidbody.DORotate(Quaternion.LookRotation(PlayerPosition - transform.position, Vector3.up).eulerAngles, _rotationDuration).SetEase(Ease.OutExpo);
    }

    private void OnDisable()
    {
        Animator.SetBool("IsAngry", false);
        _collider.isTrigger = true;
        tween.Kill();
        PeeAria.transform.parent = this.transform;       
    }

    private void Update()
    {
        if (PlayerPosition != Player.transform.position)
        {
            tween.Kill();
            PlayerPosition = Player.transform.position;
            tween = Rigidbody.DORotate(Quaternion.LookRotation
                (PlayerPosition - transform.position, Vector3.up).eulerAngles, _rotationDuration).SetEase(Ease.OutExpo);
        }

    }

}

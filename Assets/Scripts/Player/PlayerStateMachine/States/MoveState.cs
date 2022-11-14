using DG.Tweening;
using UnityEngine;

public class MoveState : State
{    
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _rotationDuration;

    private float _speed;
    private Tween tween;

    private void OnValidate()
    {
        _maxSpeed = Mathf.Clamp(_maxSpeed, 0, float.MaxValue);
        _acceleration = Mathf.Clamp(_acceleration, 0, float.MaxValue);
        _rotationDuration = Mathf.Clamp(_rotationDuration, 0, float.MaxValue);
    }

    private void OnEnable()
    {
        _speed = 0;
        Input.DirectionChangedEvent += OnDirectionChanged;        
        Animator.SetBool("Run", true);
    }

    private void OnDisable()
    {
        Input.DirectionChangedEvent -= OnDirectionChanged;
        Animator.SetBool("Run", false);
        tween.Kill();
    }



    private void OnDirectionChanged(Vector2 direction)
    {
        Rigidbody.velocity = new Vector3(direction.x, 0, direction.y).normalized * _speed;

        if (Rigidbody.velocity.magnitude != 0)
        {
            tween = Rigidbody.DORotate(Quaternion.LookRotation(Rigidbody.velocity, Vector3.up).eulerAngles, _rotationDuration).SetEase(Ease.OutExpo);
        }
    }

    private void Update()
    {
        if (_speed <= _maxSpeed)
        {
            _speed = Mathf.Lerp(_speed, _maxSpeed, Time.deltaTime * _acceleration);
        }
    }
}

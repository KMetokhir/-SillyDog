using UnityEngine;

public class CameraFolower : MonoBehaviour
{    
    [SerializeField] private float _offSetZ;
    [SerializeField] private float _speed;

    private Transform _target;


    private void Awake()
    {
        _target = FindObjectOfType<Player>().transform;
    }

    private void OnValidate()
    {
        _speed = Mathf.Clamp(_speed, 0, int.MaxValue);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(new Vector3(_target.position.x, transform.position.y, transform.position.z), GetTargetPosition(), _speed * Time.fixedDeltaTime);
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(_target.position.x, transform.position.y, _target.position.z + _offSetZ);
    }
}

using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class EnemyStateMachine : MonoBehaviour
{
    public Player Player { get; private set; }

    [SerializeField] private EnemyState _startState;

    private Animator _animator;
    private EnemyState _currentState;
    private Rigidbody _rigidbody;
    private CollisionTracker _collisionTraker;
    private PeeAria _peeAria;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _peeAria = GetComponentInChildren<PeeAria>();
        Player = FindObjectOfType<Player>();
        _collisionTraker = FindObjectOfType<CollisionTracker>();       
    }

    private void Start()
    {
        _currentState = _startState;
        _currentState.Enter(_rigidbody, _animator, Player, _peeAria, _collisionTraker);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();
        if (nextState)
            Transit(nextState);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState)
        {            
            _currentState.Enter(_rigidbody, _animator, Player, _peeAria, _collisionTraker);
        }
    }


}

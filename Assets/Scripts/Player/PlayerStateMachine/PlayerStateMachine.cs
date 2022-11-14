using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    private State _currentState;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private PlayerInput _input;
    private Player _player;
    

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _input = GetComponentInChildren<PlayerInput>();
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _currentState = _startState;
        _currentState.Enter(_rigidbody, _animator, _input, _player);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        State nextState = _currentState.GetNextState();
        if (nextState)
            Transit(nextState);

    }

    private void Transit(State nextState)
    {
        if (_currentState)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState)
            _currentState.Enter(_rigidbody, _animator, _input, _player);
    }

}

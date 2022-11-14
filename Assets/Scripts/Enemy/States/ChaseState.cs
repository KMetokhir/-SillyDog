using UnityEngine;
using UnityEngine.AI;

public class ChaseState : EnemyState
{
    private NavMeshAgent _agent;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {       
        _agent.enabled = true;
        Animator.SetBool("IsRuning", true);        
    }

    private void OnDisable()
    {
        Animator.SetBool("IsRuning", false);        
        _agent.enabled = false;        
    }

    private void Update()
    {
        _agent.SetDestination(Player.transform.position);
    }
}

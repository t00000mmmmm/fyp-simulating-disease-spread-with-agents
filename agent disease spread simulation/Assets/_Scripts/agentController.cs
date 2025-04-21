using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentController : MonoBehaviour
{
    public enum AgentState
    {
        Idle = 0,
        Patrolling,
        Chasing
    }
    public AgentState state;
    public Transform[] waypoints;
    [SerializeField] private Transform target;
    [SerializeField] private float distanceToStartHeadingToNextWaypoint;
    [SerializeField] private float distanceToPlayer = 4.0f;
    public float rotationSpeed = 2.0f;
    private int nextWaypoint;
    private NavMeshAgent navMeshAgent;
    private Animator animController;
    private int speedHashId;
    void Awake()
    {
        speedHashId = Animator.StringToHash("walkSpeed");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        if (waypoints.Length == 0)
        {
            Debug.LogError("Error: list of waypoints is empty.");
        }
    }
    void Update()
    {
        if (state == AgentState.Idle || distanceToPlayer > Vector3.Distance(target.position,gameObject.transform.position))
        {
            Idle();
        }
        else if (state == AgentState.Patrolling)
        {
            Patrol();
        }
        else
        {
            Chase();
        }
    }
    void Chase()
    {
    }
    void Idle()
    {
        
		navMeshAgent.isStopped = true;
        animController.SetFloat(speedHashId, 0.0f);
        RotateTowardsTarget();
    }
    void Patrol()
    {
        
		navMeshAgent.isStopped = false;
        animController.SetFloat(speedHashId, 1.0f);
        if (navMeshAgent.remainingDistance < distanceToStartHeadingToNextWaypoint){
            nextWaypoint = (nextWaypoint + 1 )% waypoints.Length    ;
            navMeshAgent.SetDestination(new Vector3(waypoints[nextWaypoint].position.x,waypoints[nextWaypoint].position.y,waypoints[nextWaypoint].position.z));
        
        }

    }
    void RotateTowardsTarget()
	{
		Vector3 planarDifference = (target.position - transform.position);
		planarDifference.y 	     = 0;
		Quaternion targetRotation = Quaternion.LookRotation(planarDifference.normalized);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}
}

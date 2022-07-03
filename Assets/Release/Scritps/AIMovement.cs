using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    public Transform target;
    public float UpdateRate = 0.1f;
    private NavMeshAgent agent;
    private Coroutine FollowCoroutine;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        StartChasing();
    }
    public void StartChasing()
    {
        if (FollowCoroutine == null)
        {
            FollowCoroutine = StartCoroutine(FollowTarget());
        }
        else
        {
            Debug.Log("Called StartChasing on Enemy that is already chasing!");
        }
    }
    private IEnumerator FollowTarget()
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateRate);
        while (enabled)
        {
            agent.SetDestination(target.transform.position);
            yield return wait;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement : MonoBehaviour
{
    public Vector3 target;
    public float UpdateRate = 0.1f;
    private NavMeshAgent agent;
    private Coroutine FollowCoroutine;
    public bool CanMove;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        CanMove = true;
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
        while (CanMove)
        {
            if (target == null)
            {
                agent.ResetPath();
            }
            else
            {
                agent.SetDestination(target);
            }
            yield return wait;
        }
    }
    private void OnDisable()
    {
        FollowCoroutine = null;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Point point;
    [SerializeField] public float radius;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        StartCoroutine(StartWalk());
    }
    private IEnumerator StartWalk()
    {
        while (true)
        {
            agent.SetDestination(point.RandomPointOnCircleEdge());

            yield return new WaitForSeconds(0.5f);
        }
    }
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

#endif
}

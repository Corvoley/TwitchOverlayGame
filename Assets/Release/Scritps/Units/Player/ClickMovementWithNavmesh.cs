using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]    
public class ClickMovementWithNavmesh : MonoBehaviour
{
    [SerializeField] private Camera raycastCamera;
    private NavMeshAgent agent;

    private RaycastHit[] hits = new RaycastHit[1];


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray ray = raycastCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.RaycastNonAlloc(ray,hits) > 0)
            {
                agent.SetDestination(hits[0].point);
            }

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightRadius : MonoBehaviour
{
    private Vector3 transformPos;
    public float searchRadius;
    public Collider[] targetsInRange = new Collider[10];
    public LayerMask layerMask;
    [SerializeField] private List<Transform> targetList = new List<Transform>();
    public Transform ClosestTarget;
    private Transform DefaultTarget;
    public string DefaultTargetName;
    public bool IsLookingForTarget;
    public float UpdateRate = 0.1f;
    private AIMovement movement;

    private void Awake()
    {
        movement = GetComponentInParent<AIMovement>();
        DefaultTarget = GameObject.Find(DefaultTargetName).transform;
    }
    private void Start()
    {
        StartCoroutine(TargetCheck());
    }
    private void OnEnable()
    {
        StartCoroutine(TargetCheck()) ;
        movement.target = GetRandomPositionZ(DefaultTarget);
    }

    private IEnumerator TargetCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(UpdateRate);
        while (IsLookingForTarget)
        {
            ClosestTarget = GetClosestTarget();
            
            if (ClosestTarget != null)
            {
                movement.target = ClosestTarget.position;
                ClosestTarget = GetClosestTarget();
            }
            else
            {
                movement.target = DefaultTarget.position;
            }
            
            
            yield return wait;
        }
    }

    private Transform GetClosestTarget()
    {
        
        transformPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        int targetCount = Physics.OverlapSphereNonAlloc(transformPos, searchRadius, targetsInRange, layerMask);        
        CheckIfisCorrectTarget(targetCount);
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform potentialTarget in targetList)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }
        targetList.Clear();
        return bestTarget;

    }

    private void CheckIfisCorrectTarget(int foodCount)
    {
        for (int i = 0; i < foodCount; i++)
        {
            Transform target = targetsInRange[i].transform;
            if (target != null && !targetList.Contains(target))
            {
                targetList.Add(target);
                targetsInRange[i] = null;
            }
        }
    }
    private Vector3 GetRandomPositionZ(Transform point)
    {
        var randomZ = Random.Range(-10f, 10f);
        var pos = point.position;
        pos.z += randomZ;
        return pos;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(transformPos, searchRadius);
    }
}

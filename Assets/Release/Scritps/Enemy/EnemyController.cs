using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum State
{
    Walk,
    Attack
}
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Transform wallPoint;
    [SerializeField] private Transform villagePoint;
    [SerializeField] private Vector3 wallPosition;
    private bool IsAttackingWall;
    private bool canAttack;
    [SerializeField] private float attackCD = 2f;
    private float attackCDTimer = 0;
    private float searchRadius = 10f;
    Vector3 transformPos;
    [SerializeField] private Collider[] targetsInRange = new Collider[1];
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private List<Transform> targetList = new List<Transform>();

    public State StateAI { get; private set; }

    public BaseUnit enemy { get; set; }
    private void Awake()
    {
        IsAttackingWall = true;
        agent = GetComponent<NavMeshAgent>();

        wallPoint = GameObject.Find("Wall_Left").transform;
        if (wallPoint != null)
        {
            wallPosition = GetRandomWallPosition(wallPoint);
        }
        else
        {
            IsAttackingWall = false;
        }
        WallController.OnWallDestroyed += OnWallDestroyed;

        villagePoint = GameObject.Find("villagePoint").transform;
    }

    private void Update()
    {
        AttackCooldown();
        //StateMachine();
        GetClosestTarget();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall") && canAttack)
        {
            other.GetComponent<WallController>().DecreaseHealth(1f);
            canAttack = false;
        }

    }
    private void StateMachine()
    {
        switch (StateAI)
        {
            case State.Walk:
                WalkStateHandler();
                break;
            case State.Attack:
                AttackStateHandler();
                break;
            default:
                break;
        }
    }

    private void AttackStateHandler()
    {

    }

    private void WalkStateHandler()
    {
        if (targetList.Count > 0)
        {
            agent.SetDestination(GetClosestTarget().position);            
            if (agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                StateAI = State.Attack;
            }
        }
        else if (IsAttackingWall)
        {
            agent.SetDestination(wallPosition);

        }
        else
        {
            agent.SetDestination(villagePoint.position);
        }
    }

    private void OnWallDestroyed()
    {
        IsAttackingWall = false;
    }


    private Vector3 GetRandomWallPosition(Transform wallPoint)
    {
        var randomZ = Random.Range(-10f, 10f);
        var pos = wallPoint.position;
        pos.z = randomZ;
        return pos;

    }

    private void AttackCooldown()
    {
        attackCDTimer += Time.deltaTime;
        if (attackCDTimer > attackCD)
        {
            canAttack = true;
            attackCDTimer = 0;
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
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transformPos, searchRadius);
    }

}

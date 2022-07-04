using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseUnit : PoolableObject, IDamageable
{
    public AIMovement movement;
    public NavMeshAgent agent;   

    public float health = 100f;

    [SerializeField] internal AttackRadius attackRadius;
    [SerializeField] private Animator animator;
    private Coroutine LookCoroutine;

    private const string Attack_Trigger = "Attack";
    private void Awake()
    {
        attackRadius.OnAttack += OnAttack;
    }
    private void OnAttack(IDamageable target)
    {
        Debug.Log($"Atacou o {target} ");
        animator.SetTrigger(Attack_Trigger);
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }
        LookCoroutine = StartCoroutine(LookAt(target.GetTransform()));
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    public Transform GetTransform()
    {
        return transform;
    }

    private IEnumerator LookAt(Transform target)
    {        
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0, target.position.y, 0) - new Vector3(transform.position.x, transform.position.y, transform.position.z));
        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * 2;
            yield return null;
        }
        transform.rotation = lookRotation;
    }

    public virtual void OnEnable()
    {
        SetupAgentFromConfiguration();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        agent.enabled = false;
    }
    public virtual void SetupAgentFromConfiguration()
    {
        
    }
}

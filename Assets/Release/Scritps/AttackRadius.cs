using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public SphereCollider sphereCollider;
    private List<IDamageable> damageables = new List<IDamageable>();
    public float damage = 1f;
    public float attackDelay = 0.5f;
    public delegate void AttackEvent(IDamageable target);
    public AttackEvent OnAttack;
    private Coroutine AttackCoroutine;

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageables.Add(damageable);
            if (AttackCoroutine == null)
            {
                AttackCoroutine = StartCoroutine(Attack());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageables.Remove(damageable);
            if (damageables.Count == 0)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
            }

        }
    }

    private IEnumerator Attack()
    {
        WaitForSeconds Wait = new WaitForSeconds(attackDelay);

        yield return Wait;

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;

        while (damageables.Count > 0)
        {
            for (int i = 0; i < damageables.Count; i++)
            {
                Transform damageableTransform = damageables[i].GetTransform();
                float distance = Vector3.Distance(transform.position, damageableTransform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = damageables[i];
                }
            }

            if (closestDamageable != null)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(damage);
            }

            closestDamageable = null;
            closestDistance = float.MaxValue;

            yield return Wait;

            damageables.RemoveAll(DisabledDamageables);
        }

        AttackCoroutine = null;
    }

    private bool DisabledDamageables(IDamageable Damageable)
    {
        return Damageable != null && !Damageable.GetTransform().gameObject.activeSelf;
    }
}



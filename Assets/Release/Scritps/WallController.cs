using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour, IDamageable
{
    public static event Action OnWallDestroyed;
    [SerializeField] private float health = 100;

    private void CheckHealth()
    {
        if (health < 0)
        {
            OnWallDestroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckHealth();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}

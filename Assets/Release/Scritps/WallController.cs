using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public static event Action OnWallDestroyed;
    [SerializeField] private float health = 100;

  
    public void DecreaseHealth(float value)
    {
        health -= value;
        CheckHealth();
    }


    private void CheckHealth()
    {
        if (health < 0)
        {
            OnWallDestroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseUnit
{
    public EnemyScriptableObject enemyScriptableObject;

    public override void OnEnable()
    {
        base.OnEnable();
        SetupAgentFromConfiguration();
    }
    public override void SetupAgentFromConfiguration()
    {
        base.SetupAgentFromConfiguration();
        agent.acceleration = enemyScriptableObject.Acceleration;
        agent.angularSpeed = enemyScriptableObject.AngularSpeed;
        agent.areaMask = enemyScriptableObject.AreaMask;
        agent.avoidancePriority = enemyScriptableObject.AvoidancePriority;
        agent.baseOffset = enemyScriptableObject.BaseOffset;
        agent.height = enemyScriptableObject.Height;
        agent.obstacleAvoidanceType = enemyScriptableObject.ObstacleAvoidanceType;
        agent.radius = enemyScriptableObject.Radius;
        agent.speed = enemyScriptableObject.Speed;
        agent.stoppingDistance = enemyScriptableObject.StoppingDistance;

        movement.UpdateRate = enemyScriptableObject.AIUpdateInterval;

        health = enemyScriptableObject.Health;
        (attackRadius.sphereCollider == null ? attackRadius.GetComponent<SphereCollider>() : attackRadius.sphereCollider).radius = enemyScriptableObject.AttackRadius;
        attackRadius.attackDelay = enemyScriptableObject.AttackDelay;
        attackRadius.damage = enemyScriptableObject.Damage;
    }

    
}

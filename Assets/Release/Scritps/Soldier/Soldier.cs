using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : BaseUnit
{
    public SoldierScriptableObject soldierScriptableObject;
    public override void OnEnable()
    {
        SetupAgentFromConfiguration();
    }
    public override void SetupAgentFromConfiguration()
    {
        base.SetupAgentFromConfiguration();
        agent.acceleration = soldierScriptableObject.Acceleration;
        agent.angularSpeed = soldierScriptableObject.AngularSpeed;
        agent.areaMask = soldierScriptableObject.AreaMask;
        agent.avoidancePriority = soldierScriptableObject.AvoidancePriority;
        agent.baseOffset = soldierScriptableObject.BaseOffset;
        agent.height = soldierScriptableObject.Height;
        agent.obstacleAvoidanceType = soldierScriptableObject.ObstacleAvoidanceType;
        agent.radius = soldierScriptableObject.Radius;
        agent.speed = soldierScriptableObject.Speed;
        agent.stoppingDistance = soldierScriptableObject.StoppingDistance;

        movement.UpdateRate = soldierScriptableObject.AIUpdateInterval;

        health = soldierScriptableObject.Health;

        (attackRadius.sphereCollider == null ? attackRadius.GetComponent<SphereCollider>() : attackRadius.sphereCollider).radius = soldierScriptableObject.AttackRadius;
        attackRadius.attackDelay = soldierScriptableObject.AttackDelay;
        attackRadius.damage = soldierScriptableObject.Damage;
    }
}

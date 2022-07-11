using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Point : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float circleOffSet;

    // Try to get a valid path point inside the navmesh area
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
    public Vector3 GetRandomPointInNavmesh(Transform point = null, float radius = 0)
    {        
        Vector3 _point;
        
        if (RandomPoint(point == null ? transform.position : point.position, radius == 0 ? range : radius, out _point))
        {
            Debug.DrawRay(_point, Vector3.up, Color.red, 2);

            return _point;
        }
        return point == null ? Vector3.zero : point.position;
    }
    public Vector3 RandomPointOnCircleEdge()
    {
        Vector3 vector3;       
        var vector2 = Random.insideUnitCircle.normalized * Random.Range(circleOffSet, range + circleOffSet); 
        vector3 = new Vector3(vector2.x, 0, vector2.y);
        vector3 += transform.position;
        Debug.DrawRay(vector3, Vector3.up, Color.red, 2);        
        return vector3;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range + circleOffSet );
        Gizmos.DrawWireSphere(transform.position, circleOffSet);
    }

#endif

}

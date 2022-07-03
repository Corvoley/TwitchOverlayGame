using System;
using UnityEngine;

static public class VectorExtensions
{
    static private int nearest_impl(in Vector3 point, out float sqrDistance, Vector3[] points)
    {
        if (points == null) throw new ArgumentNullException();
        if (points.Length == 0) throw new IndexOutOfRangeException();

        int index = 0;
        sqrDistance = sqrdist(point, points[0]);

        float sqrdist(in Vector3 a, in Vector3 b)
        {
            float x = a.x - b.x, y = a.y - b.y, z = a.z - b.z;
            return x * x + y * y + z * z;
        }

        if (points.Length > 1)
        {
            int hlen = points.Length >> 1;
            float sd = 0f;
            for (int i = 0, j = points.Length - 1; i <= hlen; i++, j--)
            {
                if (i < hlen || (points.Length & 1) != 0)
                {
                    if (i > 0)
                    {
                        sd = sqrdist(point, points[i]);
                        if (sd < sqrDistance) { sqrDistance = sd; index = i; }
                    }
                    if (i < hlen)
                    {
                        sd = sqrdist(point, points[j]);
                        if (sd < sqrDistance) { sqrDistance = sd; index = j; }
                    }
                }
            }
        }

        return index;
    }

    static public Vector3 NearestOf(this Vector3 point, params Vector3[] points)
      => points[nearest_impl(point, out _, points)];

    static public Vector3 NearestOf(this Vector3 point, out float distance, params Vector3[] points)
    {
        int index = nearest_impl(point, out distance, points);
        distance = Mathf.Sqrt(distance);
        return points[index];
    }

    static public Vector3 NearestOf(this Vector3 point, out int index, params Vector3[] points)
    {
        index = nearest_impl(point, out _, points);
        return points[index];
    }

    static public Vector3 NearestOf(this Vector3 point, out int index, out float distance, params Vector3[] points)
    {
        index = nearest_impl(point, out distance, points);
        distance = Mathf.Sqrt(distance);
        return points[index];
    }

    static private int farthest_impl(in Vector3 point, out float sqrDistance, Vector3[] points)
    {
        if (points == null) throw new ArgumentNullException();
        if (points.Length == 0) throw new IndexOutOfRangeException();

        int index = 0;
        sqrDistance = sqrdist(point, points[0]);

        float sqrdist(in Vector3 a, in Vector3 b)
        {
            float x = a.x - b.x, y = a.y - b.y, z = a.z - b.z;
            return x * x + y * y + z * z;
        }

        if (points.Length > 1)
        {
            int hlen = points.Length >> 1;
            float sd = 0f;
            for (int i = 0, j = points.Length - 1; i <= hlen; i++, j--)
            {
                if (i < hlen || (points.Length & 1) != 0)
                {
                    if (i > 0)
                    {
                        sd = sqrdist(points[i], points[index]);
                        if (sd > sqrDistance) { sqrDistance = sd; index = i; }
                    }
                    if (i < hlen)
                    {
                        sd = sqrdist(points[j], points[index]);
                        if (sd > sqrDistance) { sqrDistance = sd; index = j; }
                    }
                }
            }
        }

        return index;
    }

    static public Vector3 FarthestOf(this Vector3 point, params Vector3[] points)
      => points[farthest_impl(point, out _, points)];

    static public Vector3 FarthestOf(this Vector3 point, out float distance, params Vector3[] points)
    {
        int index = farthest_impl(point, out distance, points);
        distance = Mathf.Sqrt(distance);
        return points[index];
    }

    static public Vector3 FarthestOf(this Vector3 point, out int index, params Vector3[] points)
    {
        index = farthest_impl(point, out _, points);
        return points[index];
    }

    static public Vector3 FarthestOf(this Vector3 point, out int index, out float distance, params Vector3[] points)
    {
        index = farthest_impl(point, out distance, points);
        distance = Mathf.Sqrt(distance);
        return points[index];
    }

    static private int nearest_impl(in Vector3 point, out float sqrDistance, Collider[] colliders)
    {
        if (colliders == null) throw new ArgumentNullException();
        if (colliders.Length == 0) throw new IndexOutOfRangeException();

        int index = 0;
        sqrDistance = sqrdist(point, colliders[0].transform.position);

        float sqrdist(in Vector3 a, in Vector3 b)
        {
            float x = a.x - b.x, y = a.y - b.y, z = a.z - b.z;
            return x * x + y * y + z * z;
        }

        if (colliders.Length > 1)
        {
            int hlen = colliders.Length >> 1;
            float sd = 0f;
            for (int i = 0, j = colliders.Length - 1; i <= hlen; i++, j--)
            {
                if (i < hlen || (colliders.Length & 1) != 0)
                {
                    if (i > 0)
                    {
                        sd = sqrdist(point, colliders[i].transform.position);
                        if (sd < sqrDistance) { sqrDistance = sd; index = i; }
                    }
                    if (i < hlen)
                    {
                        sd = sqrdist(point, colliders[j].transform.position);
                        if (sd < sqrDistance) { sqrDistance = sd; index = j; }
                    }
                }
            }
        }

        return index;
    }

    static public Vector3 NearestOf(this Vector3 point, params Collider[] colliders)
      => colliders[nearest_impl(point, out _, colliders)].transform.position;

    static public Vector3 NearestOf(this Vector3 point, out float distance, params Collider[] colliders)
    {
        int index = nearest_impl(point, out distance, colliders);
        distance = Mathf.Sqrt(distance);
        return colliders[index].transform.position;
    }

    static public Vector3 NearestOf(this Vector3 point, out int index, params Collider[] colliders)
    {
        index = nearest_impl(point, out _, colliders);
        return colliders[index].transform.position;
    }

    static public Vector3 NearestOf(this Vector3 point, out int index, out float distance, params Collider[] colliders)
    {
        index = nearest_impl(point, out distance, colliders);
        distance = Mathf.Sqrt(distance);
        return colliders[index].transform.position;
    }


}

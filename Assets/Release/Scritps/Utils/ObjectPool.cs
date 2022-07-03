using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private PoolableObject prefab;
    private List<PoolableObject> avalibleObjects;


    private ObjectPool(PoolableObject prefab, int size)
    {
        this.prefab = prefab;
        avalibleObjects = new List<PoolableObject>(size);
    }

    public static ObjectPool CreateInstance(PoolableObject prefab, int size)
    {
        ObjectPool pool = new ObjectPool(prefab, size);
        GameObject poolObject = new GameObject(prefab.name + "Pool");
        pool.CreateObject(poolObject.transform, size);
        return pool;
    }

    private void CreateObject(Transform parent, int size)
    {
        for (int i = 0; i < size; i++)
        {
            PoolableObject poolableObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent.transform);
            poolableObject.parent = this;
            poolableObject.gameObject.SetActive(false);
        }
    }


    public void ReturnObjectToPool(PoolableObject poolableObject)
    {
        avalibleObjects.Add(poolableObject);
    }
    public PoolableObject GetObject()
    {
        if (avalibleObjects.Count > 0)
        {
            PoolableObject instance = avalibleObjects[0];
            avalibleObjects.RemoveAt(0);
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            Debug.LogError($"No {prefab.name} Object avalible at pool");
            return null;
        }

    }
}

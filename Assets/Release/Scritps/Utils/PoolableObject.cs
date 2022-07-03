using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public ObjectPool parent;
    public virtual void OnDisable()
    {
        parent.ReturnObjectToPool(this);
    }
}

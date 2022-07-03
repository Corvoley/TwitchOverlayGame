using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAt : MonoBehaviour
{
    [SerializeField] private GameObject objToFollow;

    private void Awake()
    {
        objToFollow = Camera.main.transform.gameObject;
    }

    private void LateUpdate()
    {
        transform.LookAt(transform.position + objToFollow.transform.rotation * Vector3.forward, objToFollow.transform.rotation * Vector3.up);
    }
}

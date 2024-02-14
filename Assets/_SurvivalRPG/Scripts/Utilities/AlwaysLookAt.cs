using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysLookAt : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    void Update()
    {
        transform.LookAt(_target);
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
}

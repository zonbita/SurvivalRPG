using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysRotate : MonoBehaviour
{
    [SerializeField]
    private Vector3 _axis;
    private void Update()
    {
        transform.Rotate(_axis * Time.deltaTime);
    }
}

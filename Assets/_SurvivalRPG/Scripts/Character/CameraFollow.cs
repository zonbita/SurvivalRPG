using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 Offset;
    public Transform Player;
    private void FixedUpdate()
    {
        transform.LookAt(Player.position);
    }
}

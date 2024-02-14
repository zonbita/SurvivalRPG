using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    Vector3 lookPos, lookDir;
    Ray ray;
    RaycastHit hit;
    public Transform ObjectRotation;

    private void FixedUpdate()
    {
        Rotate();
    }
    void Rotate()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        lookDir = lookPos - ObjectRotation.transform.position;
        lookDir.y = 0;

        ObjectRotation.transform.LookAt(ObjectRotation.transform.position + lookDir, Vector3.up);
    }
}

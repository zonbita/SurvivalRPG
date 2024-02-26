using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysRotationToCamera : MonoBehaviour
{
    [SerializeField] Camera camera;
    // Update is called once per frame
    void Update()
    {
        camera = Camera.main;
        if(camera != null)
        {
            this.transform.LookAt(this.transform.position + camera.transform.rotation * -Vector3.back,
                    camera.transform.rotation * -Vector3.down);
        }
    }
}

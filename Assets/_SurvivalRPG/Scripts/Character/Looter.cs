using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class Looter : MonoBehaviour
{
    CapsuleCollider capsuleCollider;
    [SerializeField] float Radius = 3;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool DrawGizmo;
    [SerializeField] float TimeCast = 2f;
    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        if(capsuleCollider != null ) capsuleCollider.isTrigger = false;

    }

    private void Start()
    {
        InvokeRepeating("SphereCast", 0, TimeCast);
    }

    IEnumerator StartSphereCast()
    {
        yield return new WaitForSeconds(TimeCast);
        SphereCast();

    }

    void SphereCast()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, Radius, Vector3.up, 1, layerMask);
        if(hits != null )
        {
            foreach( RaycastHit hit in hits)
            {
                print(hit.collider.gameObject.name);
            }
            
        }
    }

    private void OnTriggerEnter(Collider c)
    {

    }

    private void OnDrawGizmos()
    {
        if (!DrawGizmo) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, Radius);
    }
}

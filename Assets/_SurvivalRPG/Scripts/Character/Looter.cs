using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(CapsuleCollider))]
public class Looter : MonoBehaviour
{

    CapsuleCollider capsuleCollider;
    [SerializeField] float Radius = 3;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool DrawGizmo;
    [SerializeField] float TimeCast = 2f;
    private GameObject OutlineObject;
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

        if (hits != null && hits.Length > 0)
        {
            Transform t = this.transform;

            Transform closestTransform = hits
            .Select(x => x.transform)
            .OrderBy(transform => Vector3.Distance(transform.position, t.position))
            .FirstOrDefault();


            if( OutlineObject != closestTransform.gameObject)
            {
                Outline O;
                if (OutlineObject != null)
                {
                    // Check Old OutlineObject
                    O = OutlineObject.GetComponentInChildren<Outline>();
                    if (O != null) OutlineObject.GetComponentInChildren<Outline>().enabled = false;
                }

                // New OutlineObject
                OutlineObject = closestTransform.gameObject;
                O = OutlineObject.GetComponentInChildren<Outline>();

                if (O != null) O.enabled = true;
                else {
                    if(!OutlineObject && OutlineObject.transform.GetChild(1))
                    O = OutlineObject.transform.GetChild(1).AddComponent<Outline>();
                } 


                O.OutlineMode = Outline.Mode.OutlineAll;
                
            }

        }
        else
        {
            if(OutlineObject != null)
            {
                Outline O = OutlineObject.GetComponentInChildren<Outline>();
                if (O != null) OutlineObject.GetComponentInChildren<Outline>().enabled = false;

                OutlineObject = null;
            }
        }
    }

    private void OnTriggerEnter(Collider c)
    {

    }

    private void OnDisable()
    {
        if (OutlineObject != null)
        {
            Outline O = OutlineObject.GetComponentInChildren<Outline>();
            if (O != null) OutlineObject.GetComponentInChildren<Outline>().enabled = false;

            OutlineObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (!DrawGizmo) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, Radius);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public float grabRange = 3f;
    public float grabRadius = 0.5f;
    public Transform holdPoint;
    public LayerMask grabbable;
    private Rigidbody rb;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(rb == null)
                TryGrab();
            else
                Drop();
        }

        if(rb != null)
        {
            rb.MovePosition(holdPoint.position);
        }
    }

    void TryGrab()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if(Physics.SphereCast(ray, grabRadius, out hit, grabRange, grabbable))
        {
            Rigidbody rigidBody = hit.collider.GetComponent<Rigidbody>();
            if(rigidBody!=null)
            {
                rb = rigidBody;
                rb.useGravity = false;
                rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    void Drop()
    {
        rb.useGravity = true;
        //rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.None;
        rb = null;
    }
}

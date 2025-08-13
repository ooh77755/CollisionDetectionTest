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
    private SpringJoint joint;

    public float pullForce = 100f;
    public float maxDistance = 0.01f;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(rb == null)
                TryGrab();
            else
                Drop();
        }
    }

    private void FixedUpdate()
    {
        if(rb!=null)
        {
            Vector3 direction = holdPoint.position - rb.position;
            float distance = direction.magnitude;

            if(distance> maxDistance)
            {
                rb.AddForce(direction.normalized * pullForce, ForceMode.Force);
            }
        }
    }

    void TryGrab()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if(Physics.SphereCast(ray, grabRadius, out hit, grabRange, grabbable))
        {
            Rigidbody targetRB = hit.collider.GetComponent<Rigidbody>();
            if(targetRB!=null)
            {
                rb = targetRB;
                rb.useGravity = false;
                rb.isKinematic = false;
                joint = gameObject.AddComponent<SpringJoint>();
                joint.connectedBody = rb;
                joint.spring = 100f;
                joint.damper = 20f;
                joint.maxDistance = 0.5f;
                joint.enableCollision = true;
            }
        }
    }

    void Drop()
    {
        if(joint!=null)
            Destroy(joint);

        if(rb!=null)
            rb.useGravity = true;
        rb = null;
        joint = null;
    }
}

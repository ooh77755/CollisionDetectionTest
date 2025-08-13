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
    [SerializeField] SpringJoint joint;

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
            Rigidbody targetRB = hit.collider.GetComponent<Rigidbody>();
            if(targetRB!=null)
            {
                rb = targetRB;
                rb.useGravity = false;
                //joint = gameObject.AddComponent<SpringJoint>();
                joint.connectedBody = rb;
                joint.enableCollision = false;
            }
        }
    }

    void Drop()
    {
        Destroy(joint);
        rb.useGravity = true;
        rb = null;
    }
}

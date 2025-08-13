using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    public float grabRange = 3f;
    public Transform holdPoint;
    public LayerMask grabbable;
    private Transform grabbedObject;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(grabbedObject == null)
                TryGrab();
            else
                Drop();
        }

        if(grabbedObject != null)
        {
            grabbedObject.position = holdPoint.position;
        }

    }

    void TryGrab()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, grabRange, grabbable))
        {
            grabbedObject = hit.collider.transform;
            grabbedObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void Drop()
    {
        grabbedObject.GetComponent<BoxCollider>().enabled = true;
        grabbedObject.position = new Vector3(transform.position.x, transform.position.y - 0.8f, transform.position.z);
        grabbedObject = null;
    }
}

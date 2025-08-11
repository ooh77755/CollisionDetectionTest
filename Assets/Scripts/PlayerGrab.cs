using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

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
        DrawDebugSphereCast(ray.origin, ray.direction, grabRadius, grabRange, Color.red);

        if (Physics.SphereCast(ray, grabRadius, out hit, grabRange, grabbable))
        {
            DrawDebugSphereCast(ray.origin, ray.direction, grabRadius, grabRange, Color.green);
            Rigidbody rigidBody = hit.collider.GetComponent<Rigidbody>();
            if(rigidBody!=null)
            {
                rb = rigidBody;
                rb.useGravity = false;
                rb.isKinematic = false;
            }
        }
    }

    void Drop()
    {
        rb.useGravity = true;
        rb = null;
    }

    void DrawDebugSphereCast(Vector3 start, Vector3 direction, float radius, float distance, Color color)
    {
        Debug.DrawLine(start, start + direction * distance, color, 10f);
        DebugDrawWireSphere(start, radius, color);
        DebugDrawWireSphere(start + direction * distance, radius, color);
    }

    void DebugDrawWireSphere(Vector3 pos, float rad, Color col)
    {
        float step = 10f;
        for (float theta = 0; theta < 360; theta += step)
        {
            Debug.DrawLine
                (
                    pos + Quaternion.Euler(0, theta, 0) * Vector3.forward * rad,
                    pos + Quaternion.Euler(0, theta + step, 0) * Vector3.forward * rad, col, 10f
                );

            Debug.DrawLine
            (
                    pos + Quaternion.Euler(theta, 0, 0) * Vector3.up * rad,
                    pos + Quaternion.Euler(theta + step, 0, 0) * Vector3.up * rad, col, 10f
            );
        }    
    }
}

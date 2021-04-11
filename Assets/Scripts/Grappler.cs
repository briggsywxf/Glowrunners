using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    [SerializeField] private Vector2 grappleDirection = new Vector2(1000,1000);
    [SerializeField] private float grappleSpeed = 1;
    [SerializeField] private KeyCode grappleKey;
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint2D;

    private int layerMask = 1 << 6;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint2D = GetComponent<DistanceJoint2D>();
        distanceJoint2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(grappleKey))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
                grappleDirection, Mathf.Infinity, ~layerMask);
            Debug.DrawRay(transform.position, grappleDirection, Color.green, 0.5f);

            Vector2 hitPos = hit.point;
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, hitPos);
            lineRenderer.SetPosition(1, transform.position);
            distanceJoint2D.connectedAnchor = hitPos;
            distanceJoint2D.enabled = true;
        }

        if (Input.GetKey(grappleKey))
        {
            Debug.Log("grappling");
            GetComponent<DistanceJoint2D>().distance -= grappleSpeed * Time.deltaTime;
        }
        else if (Input.GetKeyUp(grappleKey))
        {
            distanceJoint2D.enabled = false;
            lineRenderer.enabled = false;
        }

        if (distanceJoint2D.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}

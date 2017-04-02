using UnityEngine;
using System.Collections;

public class cameraFollower : MonoBehaviour
{

    public float smoothTime = 0.3F;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    new Transform transform; // caching transform

    private Vector3 velocity = Vector3.zero;
       
    private void Awake() {
        transform = GetComponent<Transform>(); 
    }

    // Late update occurs after fixedUpdate(physics stuff) 
    private void LateUpdate()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (target == null)
        {
            Debug.LogWarning("Missing target ref !", this);

            return;
        }

        // compute position
        if (offsetPositionSpace == Space.Self)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.TransformPoint(offsetPosition), ref velocity, smoothTime);
            
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.TransformPoint(offsetPosition)+ offsetPosition, ref velocity, smoothTime);
        }

        // compute rotation
        if (lookAt)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.rotation = target.rotation;
        }
    }
}
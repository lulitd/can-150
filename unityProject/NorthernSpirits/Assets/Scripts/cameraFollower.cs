using UnityEngine;
using System.Collections;

public class cameraFollower : MonoBehaviour
{
    [Tooltip("Approximately the time it will take to reach the target.A smaller value will reach the target faster.")]
    public float smoothTime = 0.3F;

    [SerializeField,Tooltip("The target that this camera will follow")]
    private Transform target;

    [SerializeField, Tooltip(" Offset the camera's position")]
    private Vector3 offsetPosition;

    [SerializeField, Tooltip(" Offset the camera's Rotation"),Range(-360f,360f)]
    private float offsetRotationX;
    [SerializeField, Tooltip(" Offset the camera's Rotation"), Range(-360f, 360f)]
    private float offsetRotationY;
    [SerializeField, Tooltip(" Offset the camera's Rotation"), Range(-360f, 360f)]
    private float offsetRotationZ;

    [SerializeField,Tooltip("Is The offset position world or local space")]
    private Space offsetPositionSpace = Space.Self;
    

    new Transform transform; // caching transform

    private Vector3 velocity = Vector3.zero;
    private Quaternion offsetRotation;
       
    private void Awake() {
        transform = GetComponent<Transform>();

        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;

        offsetRotation =Quaternion.Euler(offsetRotationX, offsetRotationY, offsetRotationZ);
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


        offsetRotation = Quaternion.Euler(offsetRotationX, offsetRotationY, offsetRotationZ);
    
            transform.LookAt(target.position);
            transform.rotation*= offsetRotation ;

       


       
       
    }
}
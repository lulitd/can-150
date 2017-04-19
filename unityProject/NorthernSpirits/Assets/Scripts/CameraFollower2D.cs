using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower2D : MonoBehaviour
{

    public Transform target;

    public float dampTime = 0.1f;
    public float yDepth;

    Vector3 center = Vector3.zero;
    float interpVel;
    public Vector3 offset;
    new Transform transform;
    Camera cam;

    void Awake()
    {

        if (target == null) target = GameObject.FindGameObjectWithTag("Player").transform;

        transform = base.transform;
        transform.position = target.transform.position;
        cam = GetComponent<Camera>() ;

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.y = target.transform.position.y;
            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVel = targetDirection.magnitude * dampTime;

            center = target.transform.position + (targetDirection.normalized * interpVel * Time.deltaTime);

            Vector3 newPos = Vector3.Lerp(transform.position, center + offset, 0.25f);
            newPos.y = yDepth;
            transform.position = newPos;


        }
    }







}

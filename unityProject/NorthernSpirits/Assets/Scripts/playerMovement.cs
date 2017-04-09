using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class playerMovement : MonoBehaviour {

    public float movementSpeed;
    public float rotateSpeed = 3.0F;
    public float jumpSpeed;
    public float gravity; 

    CharacterController m_charController;

    string H_axis = "Horizontal";
    string V_axis = "Vertical";

    Vector3 moveDirection; 
    // Use this for initialization
    void Start () {
        m_charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {


        if (m_charController.isGrounded)
        {
            transform.Rotate(0, Input.GetAxis(H_axis) * rotateSpeed, 0);
            moveDirection = transform.TransformDirection(Vector3.forward);
            float curSpeed = movementSpeed * Input.GetAxis(V_axis);

            moveDirection *= curSpeed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }

        
        moveDirection.y -= gravity * Time.deltaTime;
        m_charController.Move(moveDirection * Time.deltaTime);
    }
}


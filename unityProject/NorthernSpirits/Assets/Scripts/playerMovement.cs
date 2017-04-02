using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class playerMovement : MonoBehaviour {

    public float movementSpeed;
    public float rotateSpeed = 3.0F;
    CharacterController m_charController;

    string H_axis = "Horizontal";
    string V_axis = "Vertical";

    // Use this for initialization
    void Start () {
        m_charController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Input.GetAxis(H_axis) * rotateSpeed, 0);
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = movementSpeed * Input.GetAxis(V_axis);
        m_charController.SimpleMove(forward * curSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class moveTo : MonoBehaviour {

	public Vector3 goal;

	bool LeftClick;
	Vector3 mousePos;
	public Camera mainCam;
	NavMeshAgent agent;

	void Start(){
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {
		LeftClick = Input.GetMouseButtonDown (0);
		if (LeftClick) {
			mousePos = Input.mousePosition;
			//do raycast
			RaycastHit vHit;
			Ray vRay = mainCam.ScreenPointToRay (mousePos);

			if (Physics.Raycast (vRay, out vHit)) {
				if (vHit.collider.gameObject.tag == "Ground") {
					Debug.Log ("player move!!");
					goal = vHit.point;
					agent.destination = goal;
				}
			}
		}
	}
}

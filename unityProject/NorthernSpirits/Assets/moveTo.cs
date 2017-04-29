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
		#if UNITY_STANDALONE || UNITY_WEBPLAYER
		//call TryMove()
		MouseMove();
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
		//call trymovetouch
		TouchMove();
		#endif
		MouseMove ();
	}
	void MouseMove(){
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

	void TouchMove(){
		RaycastHit hit;
		Touch touch;
		//Debug.Log (Input.touchCount);
		if (Input.touchCount > 0) {
			touch = Input.touches [0];

			if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) {
				// do ray cast when touch is detected
				//Vector2 touchPos = touch.position;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (touch.position), out hit)) {
					Debug.Log ("hit something!");
					if ((hit.collider.gameObject.tag == "Ground")) {
						goal = hit.point;
						agent.destination = goal;

					}
				}

			} 
		} 

	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalControl : MonoBehaviour {

	public RegionName teleportRegion;
	public bool lockPortal;
	private bool isThisLocked = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("player hit portal!");
			LoadRegionScene ();
		}
	}

	public void LoadRegionScene() {

		if (SceneListCheck.Has(teleportRegion.ToString()))
		{


			// TODO add map manager

			SceneManager.LoadScene(teleportRegion.ToString());

		}
		else {

			Debug.LogWarningFormat("Scene {0} does not exist. Please check build settings", teleportRegion.ToString());

		}

	}
}

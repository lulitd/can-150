using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTrigger : MonoBehaviour {

	SummonManager _sManager;
	// Use this for initialization
	void Start () {
		_sManager = this.GetComponent<SummonManager> ();
		if (_sManager == null) {
			Debug.LogError ("No summon manager script attached");
		}
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Player") {
			Debug.Log ("player ready to summon");
			_sManager.AskSummonQuestion ();
		}
	}
}

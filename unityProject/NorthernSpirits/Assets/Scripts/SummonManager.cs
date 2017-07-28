using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SummonManager : MonoBehaviour {
	//public GameObject[] SpiritsList;
	public Dictionary<RegionName, GameObject> SummonList = new Dictionary<RegionName, GameObject>();
	public Transform summonLocation;
	public Text SummonQ;
	public GameObject UI;
	GameObject spiritToSpawn = null;
	public RegionName cRegion;
	public int _MaxGemNum = 5;
	public GameObject SummonButton;
	public GameObject okButton;


	// Use this for initialization
	void Start () {
		if (SummonList.Count < 1) {
			Debug.LogWarning ("Summon List is Empty!!!");
		}
//		if(cRegion != null)
//			cRegion = RegionManager.instance.currentRegion;
	}


	private GameObject getSpirit(RegionName region){
		if (!SummonList.ContainsKey (region)) {
			Debug.LogError ("No sunch region in Summon List!");
			return null;
		} else {
			//get the spirit from the dictionary according to the region name
			IDictionaryEnumerator em = SummonList.GetEnumerator ();
			foreach (KeyValuePair<RegionName, GameObject> p in SummonList) {
				if (p.Key == region) {
					spiritToSpawn = p.Value;
				}
			}
			//check if the game object is null
			if (spiritToSpawn == null) {
				Debug.LogError ("spirit related to" + region + "is null");
				return null;
			} else {
				
				return spiritToSpawn;
			}
		}
		return null;

	}

	public void AskSummonQuestion(){
		UI.SetActive (true);
        //check if the current region can summon spirit
        if (MapManager.instance == null) return; 
		int gemNum = MapManager.instance.getNumberOfGemsCollected(cRegion);
        
		if (gemNum == _MaxGemNum) {
			GameObject sp = getSpirit (cRegion);
			SummonQ.text = "Would you like to summon " + sp.name + " from " + cRegion.ToString () + "?";
			SummonButton.SetActive (true);
			okButton.SetActive (false);
		} else {
			SummonQ.text = "You haven't collected all the gems from " + cRegion.ToString() + ". Please go back and explore more...";
			okButton.SetActive (true);
			SummonButton.SetActive (false);
		}


	}

	public void OnSummonButtonPressed(){
		//RegionName region = cRegion;
		SummonSpirit(cRegion);
		UI.SetActive (false);
		okButton.SetActive (false);
		SummonButton.SetActive (false);
	}

	public void OnOkButtonPressed(){
		UI.SetActive (false);
		okButton.SetActive (false);
		SummonButton.SetActive (false);
	}

	void SummonSpirit(RegionName region){
		GameObject spirit = getSpirit (region);
		if (spirit != null) {
			Instantiate (spirit, summonLocation.position, Quaternion.identity);
		} else {
			Debug.LogError ("Null spirit");
		}


	}
}

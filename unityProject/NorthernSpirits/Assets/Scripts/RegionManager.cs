using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour {
	//singleton

	//this class handles the region
	public int regionNum;//ENUM
	public Vector3[] GemPlacementList;

	//list of total gems in this region
	private GemObject[] regionGemList;
	private int collectedNum;

	public RegionManager Init(int RegionNum){
		regionNum = RegionNum;
		//GENERATE A SUBLIST FOR GEM PLACEMENT

		return this;
	}

	public int getNumCollected(){
		collectedNum = 0;
		for (int i = 0; i < regionGemList.Length; i++) {
			if (regionGemList [i].isCollected)
				collectedNum++;
		}


		return collectedNum;
	}

	//funtion grab data from map manager

	//function save data to map manager
}

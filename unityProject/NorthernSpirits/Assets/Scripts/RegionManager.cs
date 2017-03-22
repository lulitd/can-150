using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour {
	//this class handles the region
	public int regionNum;
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
}

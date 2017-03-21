using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemObject : MonoBehaviour {
	//this class holds the data of gems
	public int id{get;}
	public int RegionNum{ get; set;}
	public string Msg { get; set;}
	public bool isCollected{ get; set;}

	/// <summary>
	/// Init gem object with the specified msg and region number.
	/// </summary>
	/// <param name="msg">Message.</param>
	/// <param name="region">Region.</param>
	public GemObject Init(string msg, int region){
		//Constructor init the obj with a msg and a region
		id = Random.Range(0,10);//temp id
		RegionNum = region;
		Msg = msg;
		isCollected = false;

	}


	/// <summary>
	/// Init gem object with the specified region number.
	/// </summary>
	/// <param name="region">Region number.</param>
	public GemObject Init(int region){
		//Constructor with a region number
		id = Random.Range(0,10);//temp id
		RegionNum = region;
		Msg = null;
		isCollected = false;
	}
}

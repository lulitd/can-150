using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIControl : MonoBehaviour {
	//this class handles the buttons on the main menu page

	public GameObject OptionPanel;

	public void OpenOption(){
		OptionPanel.SetActive (true);
	}

	public void CloseOption(){
		OptionPanel.SetActive (false);
	}
}

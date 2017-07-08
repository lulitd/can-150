using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIControl : MonoBehaviour {
	//this class handles the buttons on the main menu page

	public GameObject OptionPanel;

	public void OpenOption(){
		OptionPanel.SetActive (true);
	}

	public void CloseOption(){
		OptionPanel.SetActive (false);
	}

	public void ToggleOption(){
		if (OptionPanel.activeInHierarchy) {
			OptionPanel.SetActive (false);
		} else {
			OptionPanel.SetActive (true);
		}
	}

	public void NewGame(){
		SceneManager.LoadScene (1);
	}

	public void Exit(){
		Application.Quit ();
	}
}

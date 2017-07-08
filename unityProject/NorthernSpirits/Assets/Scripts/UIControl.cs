using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour {
	//this class handles ui interactions
	public GameObject OptionMenu;
	public GameObject InventoryPanel;
	public GameObject[] gemList;
	public GameObject bagIcon;

	public void OpenOptionMenu(){
		OptionMenu.SetActive (true);
	}

	public void CloseOptionMenu(){
		OptionMenu.SetActive (false);
	}

	public void OpenInventory(){
		InventoryPanel.SetActive (true);
		bagIcon.SetActive (false);
	}

	public void CloseInventory(){
		InventoryPanel.SetActive (false);
		bagIcon.SetActive (true);
	}

	public void GoToTitle(){
		SceneManager.LoadScene ((int)Scenes.OpenningMenu);
	}

	public void GoToMap(){
		SceneManager.LoadScene ((int)Scenes.MenuScene);
	}


	void Update(){
		//need a function to check and show collected gems
	}
}

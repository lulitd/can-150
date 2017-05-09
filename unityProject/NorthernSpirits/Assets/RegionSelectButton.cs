using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegionSelectButton : MonoBehaviour {


    // covert all to single scriptable object?
    public RegionName Region;
    public Sprite regionImage;

    public GameObject RegionDisplay;
    public Text regionLabel;
    public Text regionStats;
    public Image imageLabel; 


    void Awake() {
        gameObject.name = "Gem_" + Region.ToString();
    }

    public void DisplayRegionInfo() {

        regionLabel.text = Region.ToString();

        RegionDisplay.SetActive(true);

    }

    public void LoadRegionScene() {

        if (SceneListCheck.Has(Region.ToString()))
        {


           // TODO add map manager

            SceneManager.LoadScene(Region.ToString());

        }
        else {

            Debug.LogWarningFormat("Scene {0} does not exist. Please check build settings", Region.ToString());

        }
        
    }
}

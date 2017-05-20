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
    private Button button; 
    private string regionString;


    void Awake()
    {
        gameObject.name = "Gem_" + Region.ToString();
        regionString = Region.ToString();

        button = GetComponent<Button>();
        button.interactable = SceneListCheck.Has(regionString);
   
    }

    public void DisplayRegionInfo() {

        regionLabel.text = regionString;

        RegionDisplay.SetActive(true);

    }

    public void LoadRegionScene() {

        if (SceneListCheck.Has(regionString))
        {


           // TODO add map manager

            SceneManager.LoadScene(regionString);

        }
        else {

            Debug.LogWarningFormat("Scene {0} does not exist. Please check build settings", regionString);

        }
        
    }
}

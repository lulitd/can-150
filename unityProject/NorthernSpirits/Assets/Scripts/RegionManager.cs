using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//this class handles the region
public class RegionManager : MonoBehaviour {
    
    //singleton
    [HideInInspector]
    public static RegionManager instance = null;

	public RegionName currentRegion;
    public List<Gem> gemsCollected = new List<Gem>(); 

    //list of total gems in this region
    private  Gem[] regionGemList;
    private  List<Gem> gemsUncollected =  new List<Gem>();

    // amount collected
    private int collectedNum;

    public void Awake() {

        // singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this) {
            Destroy(this);
            return;
        }

    }

    public void Start()
    {
        setRegion(currentRegion);
        loadData();
        SpawnManager.instance.spawnGems(gemsUncollected.ToArray());
    }

	public void setRegion(RegionName region){
        
        // attempt to load gems in region
        UnityEngine.Object[] Temp = Resources.LoadAll(region.ToString(), typeof(Gem)) ; 

        if (Temp.Length > 0)
        {
            // they get loaded as an object so we need to convert them
            regionGemList = Array.ConvertAll(Temp, item => item as Gem);

            // unloads any unused assets ie. the other regions!
            Resources.UnloadUnusedAssets();
        }

        else {
            Debug.LogWarningFormat("could not load gem data for region {0}. Data does not exist",region);
            return;
        }

        // if we got this far success!
        currentRegion = region;

        Debug.LogFormat("Region set to {0}. Contains {1} gems",currentRegion,regionGemList.Length);

    }


    public int getNumCollected(){

        collectedNum = gemsCollected.Count;

        return collectedNum;
	}

    public void CollectGem(Gem gemCollected)
    {
        gemsCollected.Add(gemCollected);
    }

    public void loadData() {

        for (int i=0; i<regionGemList.Length; i++) {
            if (regionGemList[i].isCollected) gemsCollected.Add(regionGemList[i]);
            else {
                gemsUncollected.Add(regionGemList[i]);
            }
        }



    }
    public void saveData()
    {
        // as we are using scriptable objects 
        // what ever changes we do to the object get saved automatically....
        for (int i = 0; i < gemsCollected.Count; i++) {
            gemsCollected[i].collect();
        }
    }

    //funtion grab data from map manager

    //function save data to map manager
}


public enum RegionName
{
    ALBERTA,
    BRITISH_COLUMBIA,
    MANITOBA,
    NEW_BRUNSWICK,
    NEWFOUNDLAND_LABRADOR,
    NORTHWEST_TERRITORIES,
    NOVA_SCOTIA,
    NUNAVUT,
    ONTARIO,
    PEI,
    QUEBEC,
    SASKATCHEWAN,
    YUKON
}
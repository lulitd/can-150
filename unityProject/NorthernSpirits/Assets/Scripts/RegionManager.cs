using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

//this class handles the region
public class RegionManager : MonoBehaviour {
    
    //singleton
    [HideInInspector]
    public static RegionManager instance = null;

	public RegionName currentRegion;

    //list of total gems in this region
    private  Gem[] regionGemList;

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
        SpawnManager.instance.spawnGems(regionGemList);
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

        return collectedNum;
	}

    public void CollectGem(Gem gemCollected)
    {
        collectedNum++;
        gemCollected.isCollected = true;

        saveData();
        SaveLoadManager.SavePlayerProgress();

    }

    public void loadData() {

        bool[] isCollected = MapManager.instance.map[(int)currentRegion].collectionStatus;
        for (int i=0; i<isCollected.Length; i++) {
            regionGemList[i].isCollected = isCollected[i];
        }

        // lamba expression goes through the array and counts how many are true;
        collectedNum= isCollected.Count(gem => gem);


    }
    public void saveData()
    {

        bool[] isCollected = new bool[regionGemList.Length];
            for (int i = 0; i < regionGemList.Length; i++) {
                isCollected[i] = regionGemList[i].isCollected;
            }

        MapManager.instance.map[(int)currentRegion].collectionStatus = isCollected;
    }
    
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
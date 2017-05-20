using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public static MapManager instance = null;

    private RegionData[] map;

    #if UNITY_EDITOR
    public bool resetOnLaunch;

    #endif

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

        else if (instance!= this)
        {
            Destroy(this);

        }


        #if UNITY_EDITOR
        map = (resetOnLaunch) ? SaveLoadManager.ResetPlayerProgress().regions: SaveLoadManager.LoadPlayerProgress().regions;

        #else
        
        map = SaveLoadManager.LoadPlayerProgress().regions;  

        #endif
    }

    public void Load() {

        map = SaveLoadManager.LoadPlayerProgress().regions;
    }

    public void Save()
    {

         SaveLoadManager.SavePlayerProgress();
    }

    public void Reset()
    {
        map = SaveLoadManager.ResetPlayerProgress().regions;

    }

    public bool isRegionUnlocked(RegionName region)
    { 
       return map[(int)region].isRegionLocked; 
    }

    public int getNumberOfGemsCollected(RegionName region)
    {
        return map[(int)region].numberOfGemsCollected;
    }

    public int getTotalNumberOfGems(RegionName region)
    {
        return map[(int)region].totalNumberOfGems;
    }

    public bool[] getCollectionStatus(RegionName region)
    {
        return map[(int)region].collectionStatus;
    }

    public void setCollectionStatus(RegionName region, bool[] status)
    {
        map[(int)region].collectionStatus = status;
    }

    public void setRegionUnlockStatus(RegionName region, bool status)
    {
        map[(int)region].isRegionLocked = status;
    }

    public RegionData[] getMap()
    {
        return map;
    }

}

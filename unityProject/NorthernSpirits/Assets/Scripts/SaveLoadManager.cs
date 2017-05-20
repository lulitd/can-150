using System.Collections;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public static class SaveLoadManager {

    public static void SavePlayerProgress() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        MapData map = new MapData(MapManager.instance.getMap());
        bf.Serialize(stream,map);
        stream.Close();
    }

    public static MapData LoadPlayerProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

           MapData map = bf.Deserialize(stream) as MapData;

            stream.Close();

            return map;

        }

        else
        {
            return new MapData();
        }

    }

    public static MapData ResetPlayerProgress() {

        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);


            MapData map = new MapData();
            bf.Serialize(stream, map);
            stream.Close();

            return map;

        }

        else
        {
            return new MapData();
        }

    }
}


[Serializable]
public class RegionData
{
    public bool[] collectionStatus;
    public RegionName name;
    public int totalNumberOfGems;
    public int numberOfGemsCollected;
    public bool isRegionLocked;


    public RegionData(RegionName _name, bool[] _collectionStatus) {
        collectionStatus = _collectionStatus;
        name = _name;
        totalNumberOfGems = _collectionStatus.Length;
        // lamba expression goes through the array and counts how many are true;
        numberOfGemsCollected = _collectionStatus.Count(gem => gem);
        isRegionLocked = totalNumberOfGems == numberOfGemsCollected;
    }

    public RegionData(RegionName _name, int num = 1) {
        name = _name;
        totalNumberOfGems = num;
        collectionStatus = new bool[num];
        isRegionLocked = false;
        numberOfGemsCollected = 0; 
    }
}

[Serializable]
public class MapData
{
    public RegionData[] regions;

    public MapData(RegionData[] _regions)
    {
        regions = _regions;

    }

    public MapData() {

        regions = new RegionData[13]; // 13 regions

        FileCountObject count = Resources.Load("RegionCount", typeof(FileCountObject)) as FileCountObject;


        foreach (RegionName region in Enum.GetValues(typeof(RegionName)))
        {

            int regionId = (int)region;
            regions[regionId] = new RegionData(region,count.numberOfGems[regionId]);

        }

    }
}

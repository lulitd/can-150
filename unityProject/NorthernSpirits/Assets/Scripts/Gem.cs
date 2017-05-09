using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;


[System.Serializable]
public class Gem : ScriptableObject {

    //this class holds the data of gems
    public int id = -1;
    public RegionName Region;
    public string Msg;
    public bool isCollected = false;


    public void Load(string values) {
        // parse the data
       
        // split per comma, but keeps commas in quotes. 
        string [] param = Regex.Split(values, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
        // converts string to enum
        Region = (RegionName)Enum.Parse(typeof(RegionName),param[0], true);

        // removes any quotes that were in the string. 
        Msg = param[1].Replace("\"", string.Empty);
 
    }

    public void collect()
    {
        isCollected = true;

    }

    public void reset() {
        isCollected = false;
    }

    public void setID(int newId) {
        id = newId;

    }
}




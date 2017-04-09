using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class Gem : ScriptableObject {

    //this class holds the data of gems
    public int id;
    public RegionName Region;
    public string Msg;
    public bool isCollected;


    public void Load(string values) {
        // parse the data
       
        // split per comma, but keeps commas in quotes. 
        string [] param = Regex.Split(values, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");
        // converts string to enum
        Region = (RegionName)Enum.Parse(typeof(RegionName),param[1], true);
        // string to int
        id = Convert.ToInt32(param[0]);
        // removes any quotes that were in the string. 
        Msg = param[2].Replace("\"", string.Empty);
        
        isCollected = false; 




    }
}




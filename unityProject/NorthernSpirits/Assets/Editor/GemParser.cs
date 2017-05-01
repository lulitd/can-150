using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;

public class GemParser : AssetPostprocessor
{
    static Dictionary<string, Action<string>> parsers;

    static GemParser()
    {
        parsers = new Dictionary<string, Action<string>>();
        parsers.Add("gem.csv", ParseGem);
    }

    // unity method.
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        for (int i = 0; i < importedAssets.Length; i++)
        {
            string fileName = Path.GetFileName(importedAssets[i]);
            if (parsers.ContainsKey(fileName))
                parsers[fileName](fileName);
        }
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    static void ParseGem(string itemName)
    {
        // check if the file exists
        string filePath = Application.dataPath + "/Gems/" + itemName;
        if (!File.Exists(filePath))
        {
            Debug.LogError("Missing gem Data: " + filePath);
            return;
        }

        // read the file
        string[] readText = File.ReadAllLines("Assets/Gems/" + itemName);
        

        filePath = "Assets/Gems/Generated/Resources/";


        // unity will complain if it attempts to create an asset in a folder doesn't exist.
        CheckOrCreateFolders();

        // now the magic! converting the rows of a csv to a scriptable object
        for (int i = 1; i < readText.Length; ++i)
        {
            // create the instance of the scriptable object
            Gem gemData = ScriptableObject.CreateInstance<Gem>();
            gemData.Load(readText[i]); // send it the data
                                       // save each region in its own folder
                             
            string folderPath = filePath +"/"+ gemData.Region.ToString()+"/";
            string fileName = string.Format("{0}{1}_{2}.asset", folderPath, gemData.Region.ToString(), gemData.id); 
            AssetDatabase.CreateAsset(gemData, fileName); // save the scriptable object as an asset. 
        } 
    }


   static void CheckOrCreateFolders() {

        //check and create the right folders!

        // a generated folder so we know all the assets in this folder are generated via script
        if (!AssetDatabase.IsValidFolder("Assets/Gems/Generated"))
        {
            AssetDatabase.CreateFolder("Assets/Gems", "Generated");
       
        }

        // resources folder so we can load the assets via script
        if (!AssetDatabase.IsValidFolder("Assets/Gems/Generated/Resources"))
        {
            AssetDatabase.CreateFolder("Assets/Gems/Generated", "Resources");
        }


        // a folder for each region 
        foreach (RegionName region in Enum.GetValues(typeof(RegionName)))
        {
            if (!AssetDatabase.IsValidFolder("Assets/Gems/Generated/Resources/" + region.ToString()))
            {

                AssetDatabase.CreateFolder("Assets/Gems/Generated/Resources", region.ToString());
            }

        }
            
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh(); // refresh the database otherwise we might get duplicate folders!
 
    }
}
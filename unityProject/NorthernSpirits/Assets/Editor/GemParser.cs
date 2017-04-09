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

        string folderName = System.IO.Path.GetFileNameWithoutExtension(itemName);

        filePath = "Assets/Gems/Generated/Resources/";

        // unity will complain if attempt to create an asset in a folder doesn't exist.
        //so check and make one. 
        if (!AssetDatabase.IsValidFolder("Assets/Gems/Generated"))
        {
            AssetDatabase.CreateFolder("Assets/Gems", "Generated");
        }
        
        if (!AssetDatabase.IsValidFolder("Assets/Gems/Generated/Resources"))
        {
            AssetDatabase.CreateFolder("Assets/Gems/Generated", "Resources");
        }
       
        // now the magic! converting the rows of a csv to a scriptable object
        for (int i = 1; i < readText.Length; ++i)
        {
            // create the instance of the scriptable object
            Gem gemData = ScriptableObject.CreateInstance<Gem>();
            gemData.Load(readText[i]); // send it the data
            string fileName = string.Format("{0}{1}_{2}.asset", filePath, gemData.Region.ToString(), gemData.id); 
            AssetDatabase.CreateAsset(gemData, fileName); // save the scriptable object as an asset. 
        }
    }
}
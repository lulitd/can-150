using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    // singleton pattern
    [HideInInspector]
    public static SpawnManager instance = null;

    public GameObject gemPrefab;

    Transform[] spawnPoints; // grabs spawn points from its children

    void Awake() {

        if (instance == null) {
            instance = this;
        }

        if (instance != this) {
            Destroy(this);
            return;
        }

        spawnPoints = GetComponentsInChildren<Transform>();
    }

    public void spawnGems(Gem[] gemsData)
    {
        if (!gemPrefab) {
            Debug.LogWarning("Gem prefab not set in inspector");
            return; 

        }
        for (int i = 0; i < gemsData.Length && i<spawnPoints.Length; i++) {

            // instantiate the gems. we will use the prefab rotation to insure the radar icon orientation is correct.
            // they will be parented to this object just to keep our hierachy organised. 
            GameObject gem = Instantiate(gemPrefab, spawnPoints[i].position, gemPrefab.transform.rotation, transform);

            // grab the gem data component
            GemObject gemData =gem.GetComponent<GemObject>();

            // if we have a component then set the gemdata 
            if (gemData) gemData.gemData = gemsData[i];
        }

    }
   
}

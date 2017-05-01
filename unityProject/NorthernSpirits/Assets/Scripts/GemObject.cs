using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemObject : MonoBehaviour {

   // this will get populated when it gets spawned
   public Gem gemData;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          
            if (gemData)
            RegionManager.instance.CollectGem(gemData);
            Destroy(gameObject);
            Debug.Log("item collected");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlot : MonoBehaviour {

    public Image background;
    public Image itemImage;
    public Text Label;
    public Text Description;

    public Color itemCollected;
    public Color itemUncollected;

    private Gem gemItem; 
	
    public void updateInventory(Gem gem)
    {

        itemImage.color=(gem.isCollected)? itemCollected : itemUncollected;
        gemItem =gem;
    }

    public void UpdateInventoryDescription()
    {
        Description.text = gemItem.Msg;
    }

}

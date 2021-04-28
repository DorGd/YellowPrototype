using System;
using UnityEngine;

public class MasterKey : Interactable, IHideable
{
    ItemType CurrItemType = ItemType.MasterKey; 

    public override Action[] CalcInteractions()
    {
        return new Action[] {PickUp};
    }
    
    /**
     * Pick the item to the inventory
     */
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this, true);
    }
}
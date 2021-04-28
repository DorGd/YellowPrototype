using System;
using UnityEngine;

public class Wrench : Interactable, IHideable
{
    ItemType CurrItemType = ItemType.Wrench; 
    
    public override Action[] CalcInteractions()
    {
        // no need to check if there's place because it's an hand item
        return new Action[] {PickUp};
    }
    
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this, true);
    }
    
    
}

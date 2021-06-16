using System;
using UnityEngine;

public class MasterKey : Interactable, IHideable
{
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
        
        GameManager.Instance.inventory.AddItem(this);
        gameObject.SetActive(false); // remove item from the scene        
    }
}
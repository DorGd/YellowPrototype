using System;
using UnityEngine;

public class Mineral : Interactable, IHideable
{
    public override Action[] CalcInteractions()
    {
        // there's place on the inventory
        if (GameManager.Instance.inventory.CanAdd())
        {
            return new Action[] {PickUp};
        }
        
        // there isn't place on the inventory
        return new Action[] {};
    }
    
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        // TODO - changed for testing- not an hand item any more
        GameManager.Instance.inventory.AddItem(this);
        gameObject.SetActive(false); // remove item from the scene        
    }
}

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
        
        Interactable previousHandItem = GameManager.Instance.inventory.AddItem(this, true);
        gameObject.SetActive(false); // remove item from the scene
        // had a hand object already- place it where the item we picked up was 
        if (previousHandItem != null)
        {
            GameObject previousHandItemGM = previousHandItem.gameObject; 
            previousHandItemGM.SetActive(true);
            previousHandItemGM.transform.position = transform.position;
        }
        
    }
}
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
        Interactable previousHandItem = GameManager.Instance.inventory.AddItem(this, false);
        gameObject.SetActive(false); // remove item from the scene

        // had a hand object already- place it where the item we picked up was 
        // if (previousHandItem != null)
        // {
        //     GameObject previousHandItemGM = previousHandItem.gameObject; 
        //     previousHandItemGM.SetActive(true);
        //     previousHandItemGM.transform.position = transform.position;
        // }
        
    }
}

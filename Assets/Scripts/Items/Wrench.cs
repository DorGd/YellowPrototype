using System;
using UnityEngine;

public class Wrench : Interactable, IHideable
{
   
    public override Action[] CalcInteractions()
    {
        // no need to check if there's place because it's an hand item
        return new Action[] {PickUp};
    }
    
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        Interactable previousHandItem = GameManager.Instance.inventory.AddItem(this, true);

        // had a hand object already- place it where the item we picked up was 
        if (previousHandItem != null)
        {
            GameObject previousHandItemGM = previousHandItem.gameObject; 
            previousHandItemGM.SetActive(true);
            previousHandItemGM.transform.position = transform.position;
        }
        
    }
    
    
}

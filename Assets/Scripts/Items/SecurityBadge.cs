using System;
using UnityEngine;

public class SecurityBadge : Interactable, IHideable
{
    public override Action[] CalcInteractions()
    {
        // there's place on the inventory
        if (GameManager.Instance.inventory.CanAdd())
        {
            return new Action[] {PickUp};
        }
        
        // there isn't place on the inventory
        return new Action[] {};    }

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

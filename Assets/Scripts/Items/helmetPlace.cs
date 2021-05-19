using System;
using UnityEngine;

public class HelmetPlace : Interactable
{
    public override Action[] CalcInteractions()
    {
        if (GameManager.Instance.inventory.IsInInventory(ItemType.Helmet, false))
        {
            return new Action[] {Place};
        }

        return new Action[] {};
    }
    
    /**
     * place the helmet back down
     * TODO need to add this option
     */
    public void Place()
    {
        Debug.Log("Place");
        
        GameManager.Instance.inventory.DeleteItem(ItemType.Helmet); 
    }
}

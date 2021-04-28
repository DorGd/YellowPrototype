using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Vent : Interactable
{
    ItemType CurrItemType = ItemType.Vent;

    public bool open = false;

    public override Action[] CalcInteractions()
    {
        if (!open && GameManager.Instance.inventory.IsInInventory(ItemType.Wrench, true))
        {
            return new Action[] {Open};
        }
        
        return new Action[] {};
    }
    
    /**
     * Open the door
     */
    public void Open()
    {
        Debug.Log("Open");
        open = true; 
    }
}

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Vent : Interactable
{

    public bool open = false;

    public override Action[] CalcInteractions()
    {
        if (!open && GameManager.Instance.inventory.IsInInventory(ItemType.Wrench, true))
        {
            Debug.Log("found wrench");

            return new Action[] {Open};
        }
        
        Debug.Log("No wrench");
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

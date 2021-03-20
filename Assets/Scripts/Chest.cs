using System;
using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool open = false; 
    public Action[] CalcInteractions()
    {
        return new Action[] {PickUp, OpenChest};
    }
    
    void PickUp()
    {

        Debug.Log("Pickup");

        // if (!open)
        // {
        //     Debug.Log("Pickup");
        //     GameManager.Instance.inventory.AddItem(this);
        //
        //     // TODO need to implement if the object is held in the players hand or in the inventory
        //     gameObject.SetActive(false);
        // }
    }
    
    void OpenChest()
    {
        Debug.Log("OpenChest");

        // if the inventory has something
        // open = true; 
    }
}
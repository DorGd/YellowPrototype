using System;
using UnityEditor;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool open = false;
    public GameObject key; 
    
    public Action[] CalcInteractions()
    {
        // have the key and the chest is closed
        if (GameManager.Instance.inventory.IsInInventory(key) && !open)
        {
            return new Action[] {PickUp, OpenChest};
        }
        
        // doesn't have the key and the chest is closed
        if (!GameManager.Instance.inventory.IsInInventory(key) && !open)
        {
            return new Action[] {PickUp};
        }
        
        // nothing to do
        return new Action[] { };
    }
    
    void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this.gameObject);
        
        // TODO need to implement if the object is held in the players hand or in the inventory
        gameObject.SetActive(false);
        
    }
    
    void OpenChest()
    {
        Debug.Log("OpenChest");
        
        open = true;
    }
}
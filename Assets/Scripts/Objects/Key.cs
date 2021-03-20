using System;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    
    public Action[] CalcInteractions()
    {
        return new Action[] {PickUp};
    }
    
    void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this.gameObject);
        
        // TODO need to implement if the object is held in the players hand or in the inventory
        gameObject.SetActive(false);
        
    }
}
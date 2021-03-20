using System;
using UnityEngine;

public class helmetPlace : MonoBehaviour, IInteractable
{
    public GameObject helmet;

    public Action[] CalcInteractions()
    {
        if (GameManager.Instance.inventory.IsInInventory(helmet, false))
        {
            return new Action[] {Place};
        }

        return new Action[] {};
    }
    
    public void Place()
    {
        Debug.Log("Place");
        
        // for now is supposed to go back to wear it was before inserted into the inventory- this place
        GameManager.Instance.inventory.RemoveItem(this.gameObject); 
    }
}

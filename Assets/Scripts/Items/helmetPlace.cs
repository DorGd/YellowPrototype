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
        
        // TODO need to make the helmet disappear from player head
        GameManager.Instance.inventory.RemoveItem(helmet); 
    }
}

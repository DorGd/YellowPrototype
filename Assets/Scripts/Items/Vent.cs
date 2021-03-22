using System;
using UnityEngine;

public class Vent : MonoBehaviour, IInteractable
{
    
    public bool open = false;
    public GameObject[] wrenches; 
    
    public Action[] CalcInteractions()
    {
        foreach (GameObject item in GameManager.Instance.inventory.inventoryItems)
        {
            if (!open && item != null && item.name.StartsWith("Wrench"))
            {
                return new Action[] { Open };
            }
        }        
        
        return new Action[] {};
    }
    
    public void Open()
    {
        Debug.Log("Open");
        open = true;
        gameObject.SetActive(false);
    }
}

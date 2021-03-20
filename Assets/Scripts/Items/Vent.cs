using System;
using UnityEngine;

public class Vent : MonoBehaviour, IInteractable
{
    
    public bool open = false;
    public GameObject wrench; 
    
    public Action[] CalcInteractions()
    {
        if (!open && GameManager.Instance.inventory.IsInInventory(wrench, true))
        {
            return new Action[] {Open};
        }
        return new Action[] {};
    }
    
    public void Open()
    {
        Debug.Log("Open");
        open = true; 
    }
}

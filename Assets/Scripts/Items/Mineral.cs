using System;
using UnityEngine;

public class Mineral : MonoBehaviour, IInteractable, IHideable
{
    
    public Action[] CalcInteractions()
    {
        return new Action[] {PickUp};
    }
    
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this.gameObject, true);
    }
}

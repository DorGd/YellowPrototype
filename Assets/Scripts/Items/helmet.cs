using System;
using UnityEngine;

public class helmet : MonoBehaviour, IInteractable
{
    public Action[] CalcInteractions()
    {
        return new Action[] {Wear};
    }
    
    public void Wear()
    {
        Debug.Log("Wear");
        
        GameManager.Instance.inventory.AddItem(this.gameObject, false);
        gameObject.SetActive(false);
    }
}

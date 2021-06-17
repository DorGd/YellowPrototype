using System;
using UnityEngine;

public class Helmet : Interactable, IHideable
{
    
    public override Action[] CalcInteractions()
    {
        return new Action[] {Wear};
    }
    
    /**
     * Wear the helmet
     * TODO need to add a visual (maybe not add to inventory? or create some kind of visual inventory)
     */
    public void Wear()
    {
        Debug.Log("Wear");
        PickUp();
    }

    public void PickUp()
    {
        GameManager.Instance.inventory.AddItem(this);
    }
}

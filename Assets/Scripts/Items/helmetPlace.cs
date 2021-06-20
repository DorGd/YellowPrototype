using System;
using UnityEngine;

public class HelmetPlace : Interactable
{
    public Helmet[] helmets;
    public override Action[] CalcInteractions()
    {
        if (GameManager.Instance.inventory.IsInInventory(ItemType.Helmet, false))
        {
            return new Action[] {Place};
        }

        return new Action[] {};
    }

    public void DisableHelmets()
    {
        foreach (Helmet helmet in helmets)
        {
            if (helmet.gameObject.activeSelf)
            {
                helmet.gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }
    }
    
    /**
     * place the helmet back down
     * TODO need to add this option
     */
    public void Place()
    {
        Debug.Log("Place");

        foreach (Helmet helmet in helmets)
        {
            if (helmet.gameObject.activeSelf)
            {
                helmet.gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
        }

        GameManager.Instance.inventory.DeleteItem(ItemType.Helmet); 
        foreach(Helmet helmet in helmets)
        {
            if (!helmet.gameObject.activeSelf)
            {
                helmet.gameObject.SetActive(true);
                return;
            }
        }
    }
}

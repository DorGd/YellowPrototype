using System;
using UnityEngine;

public class SecurityBadge : Interactable, IHideable, IHideable.IWearable
{
    public override Action[] CalcInteractions()
    {
        return new Action[] { PickUp };
    }

    public void PickUp()
    {
        Debug.Log("Pickup");
        // there's place on the inventory
        if (!GameManager.Instance.inventory.IsInInventory(ItemType.SecurityBadge) && GameManager.Instance.inventory.CanAdd())
        {
            GameManager.Instance.inventory.AddItem(this);
            return;
        }
        GameManager.Instance.SpeechManager.StartSpeech(transform.position, new string[] { "You already have a badge" }, true);
    }

}

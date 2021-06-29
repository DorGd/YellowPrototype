using System;
using System.Collections;
using UnityEngine;

public class MasterKey : Interactable, IHideable
{
    private bool firstHold = false;
    public override Action[] CalcInteractions()
    {
        return new Action[] {PickUp};
    }
    
    /**
     * Pick the item to the inventory
     */
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this);
        if (!firstHold)
        {
            AudioManager.Instance.PlayMusicForTime(AudioManager.Music_stealth, 10f);
            firstHold = true;
        }
        gameObject.SetActive(false); // remove item from the scene        
    }
}
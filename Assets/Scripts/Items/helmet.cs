using System;
using UnityEngine;

public class Helmet : Interactable, IHideable, IHideable.IWearable
{
    public HelmetPlace helmetPlace;
    public SpeechTextSO cantWearText;
    public override Action[] CalcInteractions()
    {
        if (!GameManager.Instance.inventory.IsInInventory(ItemType.Helmet) && GameManager.Instance.inventory.CanAdd())
        {
            return new Action[] { Wear };
        }
        GameManager.Instance.SpeechManager.StartSpeech(transform.position, cantWearText);
        return new Action[] {};
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

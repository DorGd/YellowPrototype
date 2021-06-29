using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Vent : Interactable
{

    public bool open = false;

    public override Action[] CalcInteractions()
    {
        return new Action[] {Open};
    }
    
    /**
     * Open the door
     */
    public void Open()
    {
        if (!open && GameManager.Instance.inventory.IsInInventory(ItemType.Wrench))
        {
            Debug.Log("Open");
            open = true;
            gameObject.SetActive(false);
            AudioManager.Instance.PlayOneShot(AudioManager.SFX_metalClank);
        }
        else
        {
            AudioManager.Instance.PlayOneShot(AudioManager.SFX_failedInteraction, 0.5f);
            GameManager.Instance.SpeechManager.StartSpeech(transform.position, new string[] { "The vent is bolted shut" });
        }

    }
}

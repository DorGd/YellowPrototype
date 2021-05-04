using System;
using UnityEngine;

public class MasterKey : MonoBehaviour, IInteractable, IHideable
{
    public AudioClip music;
    
    public Action[] CalcInteractions()
    {
        return new Action[] {PickUp};
    }
    
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        GameManager.Instance.inventory.AddItem(this.gameObject, true);
        Camera.main.GetComponent<AudioSource>().clip = music;
        Camera.main.GetComponent<AudioSource>().Play();
    }
}
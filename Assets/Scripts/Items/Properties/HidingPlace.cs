using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HPInventory))]
public abstract class HidingPlace : Interactable
{
    public HPInventory hpInventory;
    public bool open = false;

    new void Start()
    {
        base.Start();
        hpInventory = GetComponent<HPInventory>();

    }
    public abstract void Hide();
    public abstract void Close();
    public abstract void Open();
    public abstract void Exchange();
    
}

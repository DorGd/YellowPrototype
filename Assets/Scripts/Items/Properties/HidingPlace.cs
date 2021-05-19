using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HPInventory))]
public abstract class HidingPlace : Interactable
{
    public HPInventory inventory;
    public bool open = false;

    new void Start()
    {
        base.Start();
        inventory = GetComponent<HPInventory>();

    }
    public abstract void Hide();
    public abstract void Close();
    public abstract void Open();
    public abstract void Show();

}

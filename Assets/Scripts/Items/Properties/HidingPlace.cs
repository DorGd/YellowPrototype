using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HPInventory))]
public abstract class HidingPlace : Interactable
{
    public HPInventory hpInventory;
    public bool open = false;

    protected override void Start()
    {
        base.Start();
        hpInventory = GetComponent<HPInventory>();

    }

    private void Update()
    {
        if ((GameManager.Instance.PlayerAI.transform.position - transform.position).magnitude > 1)
        {
            FindObjectOfType<InventoryUI>().StopExchange();
        }
    }

    public abstract void Hide();
    public abstract void Close();
    public abstract void Open();
    public abstract void Exchange();
    
}

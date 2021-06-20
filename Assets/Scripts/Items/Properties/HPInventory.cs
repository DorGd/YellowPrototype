 using System;
 using System.Collections;
using UnityEngine;

 public class HPInventory : Inventory
 {
    public HPInventoryUI _inventoryUI;
    private string _inventoryName;
    private bool _inExchange = false;

    private void Start()
    {
        // find the shared UI panel 
        _inventoryUI = GameObject.Find("HP inventory (canvas)").GetComponent<HPInventoryUI>();
    }

    private void Update()
    {
        if (_inExchange && (GameManager.Instance.PlayerAI.transform.position - transform.position).magnitude > 2)
        {
            FindObjectOfType<InventoryUI>().StopExchange();
        }
    }

    public void StartExchange(string name)
    {
        _inExchange = true;
        _inventoryUI.Load_Inventory(name, this);
        _inventoryUI.OpenInventory();
    }

    public void StopExchange()
    {
        _inExchange = false;
    }

    /**
    * add to the inventory
    */
    public override void AddItem(Interactable newItem, InventorySlot fromSlot = null)
    {
        if (!(newItem is IHideable))
            return;
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] == null)
            {
                // add to the free space
                InventoryItems[i] = newItem;
                InventoryCount++;
                Debug.Log(InventoryItems[i].GetItemType());

                if (_inExchange)
                    _inventoryUI.AddItem(newItem); // Add the item to UI
                return;
            }
        }
    }

    /**
    * Delete an item
    */
    public override void DeleteItem(ItemType item, int slot = -1)
    {
        if (slot >= 0)
        {
            if (InventoryItems[slot] != null && InventoryItems[slot].GetItemType() == item)
            {
                if (_inExchange)
                    _inventoryUI.Removeitem(item, slot); // remove the item to UI
                InventoryItems[slot] = null;
                InventoryCount--;
                return;
            }
        }
        for (int i = 0; i < InventoryItems.Length; i++)
        {
            if (InventoryItems[i] != null && InventoryItems[i].GetItemType() == item)
            {
                if (_inExchange)
                    _inventoryUI.Removeitem(item, i); // remove the item to UI
                InventoryItems[i] = null;
                InventoryCount--;
                return;
            }
        }
    }

    public override bool CanAdd()
    {
        return _inExchange && base.CanAdd();
    }
}

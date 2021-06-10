using System;
using UnityEngine;

public class Chest : HidingPlace, IHideable
{
    private Interactable _curHandItem;

    /**
     * if chest is open:
     *     if we have wrench as a hand object- we can close the chest 
     *     if we have other object as a hand object- we can put it in the chest
     * if chest is close:
     *     we can pick it up as hand pbject
     */
    public override Action[] CalcInteractions()
    {
        _curHandItem = GameManager.Instance.inventory.GetHandItem();
        
        // chest is open
        if (open)
        {
            // we have a wrench in the hand
            if (GameManager.Instance.inventory.IsInInventory(ItemType.Wrench, true))
            {
                return new Action[] {Close, Hide, Exchange};
            }
            
            // we have another item in hand
            if (_curHandItem != null)
            {
                return new Action[] {Hide, Exchange};
            }
            // Chest is open and there is nothing to hide
            return new Action[] {Exchange};
        }

        // chest is closed
        // don't need to check if there's a place for the items because this is a hand items and can always be picked up
        return new Action[] {PickUp, Open};
    }
    
    /**
     * pick the chest up as hand object
     */
    public void PickUp()
    {
        Debug.Log("Pickup");
        
        Interactable previousHandItem = GameManager.Instance.inventory.AddItem(this, true);

        // had a hand object already- place it where the item we picked up was 
        if (previousHandItem != null)
        {
            GameObject previousHandItemGM = previousHandItem.gameObject; 
            previousHandItemGM.SetActive(true);
            previousHandItemGM.transform.position = transform.position;
        }
        
    }
    
    /**
     * open the chest
     */
    public override void Open() 
    {
        Debug.Log("Open chest");
        
        open = true;
    }
    
    /**
     * close the chest
     */
    public override void Close()
    {
        Debug.Log("Close chest");
        
        open = false;
    }

    /**
     * hide the current hand item in the chest
     */
    public override void Hide()
    {
        Debug.Log("Hide hand item in chest");

        GameManager.Instance.inventory.DeleteItem(_curHandItem.GetItemType()); // remove from global inventory
        hpInventory.AddItem(_curHandItem, false); // add to local inventory
    }
    
    /**
     * Show the items in the inventory
     */
    public override void Exchange()
    {
        Debug.Log("Show the items in the chest");
        
        // Handling the local inventory 
        // todo - the problem is in the load function 
        hpInventory._inventoryUI.Load_Inventory("Chest", hpInventory); // load the items to the inventory 
        
        // Handling the main inventory 
        MainInventory main = GameManager.Instance.inventory;
        
        // open both inventories panels
        main.GetInventoryUI().StartExchange();
    }

}
using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    // manage UI
    public InventorySlot[] slots = new InventorySlot[4]; 
    public InventorySlot handItemSlot;   

    // only UI components
    public GameObject inventoryPanel;
    public GameObject inventoryButton;
    public GameObject closeButtonMain;
    public GameObject closeButtonHP;
    public GameObject stopExchange;

    /**
     * Add an item to the inventory slot
     */
    public void AddItem(Interactable item, bool toHand = false)
    {
        if (toHand)
        {
            handItemSlot.AddItem(item);
            return;
        }
        
        foreach (InventorySlot slot in slots)
        {
            // Can add this item to the inventory 
            if (slot.IsEmpty())
            {
                slot.AddItem(item);
                break;
            }
        }
    }
    
    /**
     * Remove the item from the UI.
     */
    public void Removeitem(ItemType item, bool fromHand = false)
    {
        if (fromHand && !handItemSlot.IsEmpty() && handItemSlot.Contains() == item)
        {
            handItemSlot.RemoveItem();
            return;
        }
        
        foreach (InventorySlot slot in slots)
        {
            // Can add this item to the inventory 
            if (!slot.IsEmpty() && slot.Contains() == item)
            {
                slot.RemoveItem();
                break;
            }
        }
    }


    /**
     * open the inventory panel 
     */
    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        inventoryButton.SetActive(false);
    }

    /**
     * close the inventory panel
     */
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        inventoryButton.SetActive(true);

    }
    
    // Handle exchange
    
    /**
     * disable the close buttons on the inventories.
     * Is called when Exchange button in pressed.
     */
    public void StopExchange()
    {
        closeButtonMain.SetActive(true);
        closeButtonHP.SetActive(true);
        stopExchange.SetActive(false);

        CloseInventory();
        FindObjectOfType<HPInventoryUI>().CloseInventory();

    }
    
    /**
     * disable the close buttons on the inventories.
     * Is called when stop Exchange button in pressed.
     */
    public void StartExchange()
    {
        closeButtonMain.SetActive(false);
        closeButtonHP.SetActive(false);
        stopExchange.SetActive(true);

        FindObjectOfType<HPInventoryUI>().OpenInventory();
        OpenInventory();
    }
}

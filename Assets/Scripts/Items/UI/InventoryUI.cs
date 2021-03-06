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
    
    public event Action onFirstExchange;

    /**
     * Add an item to the inventory slot
     */
    public void AddItem(Interactable item, bool toHand = false)
    {
        if (toHand)
        {
            AudioManager.Instance.PlayOneShot(AudioManager.SFX_interactionMenuPopup, 0.5f);
            handItemSlot.AddItem(item);
            return;
        }
        
        foreach (InventorySlot slot in slots)
        {
            // Can add this item to the inventory 
            if (slot.IsEmpty())
            {
                AudioManager.Instance.PlayOneShot(AudioManager.SFX_interactionMenuPopup, 0.5f);
                slot.AddItem(item);
                break;
            }
        }
    }
    
    /**
     * Remove the item from the UI.
     */
    public void RemoveItem(ItemType item, bool fromHand = false, int slotIndex = -1)
    {
        if (fromHand && !handItemSlot.IsEmpty() && handItemSlot.GetItemType() == item)
        {
            AudioManager.Instance.PlayOneShot(AudioManager.SFX_interactionMenuPopup, 0.5f);
            handItemSlot.RemoveItem();
            handItemSlot.GetComponent<Animator>().SetTrigger("Updated");
            return;
        }

        if (slotIndex >= 0)
        {
            Debug.Log(gameObject.name);
            // Can remove this item from the inventory 
            if (!slots[slotIndex].IsEmpty() && slots[slotIndex].GetItemType() == item)
            {
                AudioManager.Instance.PlayOneShot(AudioManager.SFX_interactionMenuPopup, 0.5f);
                slots[slotIndex].RemoveItem();
                if (slots[slotIndex].gameObject.activeInHierarchy)
                {
                    slots[slotIndex].GetComponent<Animator>().SetTrigger("Updated");
                }
                else
                {
                    GameObject inventoryBtn = GameObject.Find("Inventory Button ");
                    inventoryBtn.GetComponent<Animator>()?.SetTrigger("Updated");
                }
                return;
            }
        }

        foreach (InventorySlot slot in slots)
        {
            // Can add this item to the inventory 
            if (!slot.IsEmpty() && slot.GetItemType() == item)
            {
                AudioManager.Instance.PlayOneShot(AudioManager.SFX_interactionMenuPopup, 0.5f);
                slot.RemoveItem();
                if (slot.gameObject.activeInHierarchy)
                {
                    slot.GetComponent<Animator>().SetTrigger("Updated");
                }
                else
                {
                    GameObject inventoryBtn = GameObject.Find("Inventory Button ");
                    inventoryBtn.GetComponent<Animator>()?.SetTrigger("Updated");
                }
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
        onFirstExchange?.Invoke();
        closeButtonMain.SetActive(false);
        closeButtonHP.SetActive(false);
        stopExchange.SetActive(true);

        FindObjectOfType<HPInventoryUI>().OpenInventory();
        OpenInventory();
    }
}

 using System;
 using System.Collections;
using UnityEngine;

 public class HPInventory : Inventory
 {
     public HPInventoryUI _inventoryUI;
     private string _inventoryName;

     private void Start()
     {
         // find the shared UI panel 
         _inventoryUI = GameObject.Find("HP inventory (canvas)").GetComponent<HPInventoryUI>();
     }


     /**
     * add to the inventory
     */
     public override Interactable AddItem(Interactable newItem, bool toHand = false)
     {
         for (int i = 0; i < InventoryItems.Length; i++)
         {
             if (InventoryItems[i] == null)
             {
                 // add to the free space
                 InventoryItems[i] = newItem;
                 InventoryCount += 1;
                 Debug.Log(InventoryItems[i].item._type);

                 _inventoryUI.AddItem(newItem); // Add the item to UI
                 return null;
             }
         }
         return null;
     }

     /**
     * Delete an item
     */
     public override void DeleteItem(ItemType item)
     {
         
         for (int i = 0; i < InventoryItems.Length; i++)
         {
             if (InventoryItems[i] != null && InventoryItems[i].GetItemType() == item)
             {
                 _inventoryUI.Removeitem(item); // remove the item to UI
                 InventoryItems[i] = null;
                 InventoryCount--; 
             }
         }
     } 
     
}

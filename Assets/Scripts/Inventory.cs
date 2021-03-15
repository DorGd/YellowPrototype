using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;
    private void Awake()
    {
        instance = this;
    }

    // need to be hand item 
    public GameObject currentItemGM = null; 

    // TODO list of inventory items- add the currnt one also 
    
    
    // TODO add search function 
    public void AddItem(GameObject newGM)
    {
        
        // if there is an item already - put it down and put a new one
        if (currentItemGM != null)
        {
            RemoveItem(); 
        }
        
        currentItemGM = newGM;
    }

    public void RemoveItem()
    {
        // place the item next to the player
        if (currentItemGM != null)
        {
            currentItemGM.SetActive(true); // the item is back to where we found it
            currentItemGM = null;    
        }
    }
}


using UnityEngine;

public class Pickup : Interactable
{
    
    public override void Interact()
    {
        base.Interact();  // run the original Interact function

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Pickup");
        Inventory.instance.AddItem(this.gameObject);
        
        // TODO need to implement if the object is held in the players hand or in the inventory
        gameObject.SetActive(false);
    }
}
using System;
using UnityEngine;

public class SecurityBadge : MonoBehaviour, IInteractable, IHideable
{
    public Action[] CalcInteractions()
    {
        return new Action[] {PickUp};
    }

    public void PickUp()
    {
        Debug.Log("Pickup");
        GameManager.Instance.inventory.AddItem(this.gameObject, true);
        gameObject.SetActive(false);
    }

}

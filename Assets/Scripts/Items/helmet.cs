using System;
using UnityEngine;

public class helmet : MonoBehaviour, IInteractable
{
    public GameObject hanger;
    public Action[] CalcInteractions()
    {
        return new Action[] {Wear};
    }
    
    public void Wear()
    {
        Debug.Log("Wear");
        //GameObject.Find("LivingQuartersSecurityDoorContainer")?.GetComponent<SecurityDoor>().StopCountdown();
        //GameObject.Find("LivingQuartersDoorContainer")?.GetComponent<SecurityDoor>().StopCountdown();
        // TODO need to make the helmet appear on the player head
        GameManager.Instance.inventory.AddItem(this.gameObject, false);
        gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").transform.Find("helmet").gameObject.SetActive(true);
        hanger.SetActive(true);
    }
}

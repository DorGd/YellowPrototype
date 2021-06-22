using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spaceship : Interactable
{

    public override Action[] CalcInteractions()
    {
        return new Action[] {Escape};
    }
    
    /**
     * Open the door
     */
    public void Escape()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        GameManager.Instance.PlayerAI.transform.parent = transform;
        GameManager.Instance.PlayerAI.gameObject.SetActive(false);
        GameObject.Find("HangarBayDoors").GetComponent<Animator>().SetTrigger("Open");
        GetComponent<Animator>()?.SetTrigger("Escape");
    }
}

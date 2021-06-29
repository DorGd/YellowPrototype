using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

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
        GameManager.Instance.PlayerAI.GetComponent<Controller>().FreezeController();
        GameManager.Instance.PlayerAI.GetComponent<NavMeshAgent>().enabled = false;
        GameManager.Instance.Clock.enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        GameManager.Instance.PlayerAI.transform.parent = transform;
        foreach (Transform child in GameManager.Instance.PlayerAI.transform)
        {
            child.gameObject.SetActive(false);
        }
        GameObject.Find("HangarBayDoors").GetComponent<Animator>().SetTrigger("Open");
        AudioManager.Instance.PlayOneShot(AudioManager.SFX_liftoff);
        GetComponent<Animator>()?.SetTrigger("Escape");
        GameManager.Instance.EndLevel();
    }
}

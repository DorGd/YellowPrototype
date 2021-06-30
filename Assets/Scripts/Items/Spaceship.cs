using System;
using System.Collections;
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
        StartCoroutine(EscapeCoroutine());
    }

    private IEnumerator EscapeCoroutine()
    {
        GetComponent<Animator>()?.SetTrigger("Escape");
        AudioManager.Instance.PlayOneShot(AudioManager.Music_liftoff);
        yield return new WaitForSeconds(25f);
        GameManager.Instance.EndLevel();
    }
}

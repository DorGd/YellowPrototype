using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
   
    private Transform _player;

    
    public virtual void Interact()
    {
        // implement for every interactable 
        Debug.Log("Interact");

    }

}

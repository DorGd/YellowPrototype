using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    
    
    private bool _interacting = false; // currently interacting 
    public bool interacted = false; // has interacted
    private Transform _player;

    
    public virtual void Interact()
    {
        // implement for every interactable 
        Debug.Log("Interact");

    }
    
    // TODO add pickup 
    // TODO add interaction between interactables
    // TODO leave object
    // TODO hover over an interactable

    private void Update()
    {
        
        if (_interacting)
        {
            float dist = Vector3.Distance(_player.position, transform.position);

            if (dist <= radius && !interacted)
            {
                interacted = true;
                Interact();
            }
        }
    }
    

    public void OnInteraction(Transform playersTransform)
    {
        _interacting = true;
        interacted = false;
        _player = playersTransform;
    }
    
    public void OnDeInteraction()
    {
        _interacting = false;
        interacted = false;
        _player = null;
    }
    
    
    // visualize the radius of our player stop location
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

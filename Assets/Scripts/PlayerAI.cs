using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    Transform follow; // the position of the interactable object to follow 

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    { 
        if (follow != null)
        {
            _agent.SetDestination(follow.position);
        }
    }

    public void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }

    public void FollowInteractable(Interactable interactable)
    {
        _agent.stoppingDistance = interactable.radius * 0.8f;
        follow = interactable.transform; 
    }

    public void StopFollowingInteractable()
    {
        // TODO add this and see how it still stops far from the object we ant to interact with 
        // _agent.stoppingDistance = 0f;
        follow = null;
    }
  }

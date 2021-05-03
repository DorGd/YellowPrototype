using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerAI : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform[] WayPoints;
    private bool _patroling = false;
    public bool Patroling
    {
        get { return _patroling; }
        set { _patroling = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    public void MoveToPoint(Vector3 point)
    {
        _agent.SetDestination(point);
    }
}

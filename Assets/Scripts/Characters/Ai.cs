using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Ai : MonoBehaviour
{
    private NavMeshAgent _agent; 
    public Transform[] WayPoints;
    private bool _patroling = false;
    private Controller _controller;

    public bool Patroling
    {
        get { return _patroling; }
        set { _patroling = value; }
    }

    internal void ForceMoveToPoint(Vector3 sendToPosition)
    {
        StartCoroutine(ForceMoveCoroutine(sendToPosition));
    }

    private IEnumerator ForceMoveCoroutine(Vector3 sendToPosition)
    {
        yield return new WaitForEndOfFrame();
        gameObject.layer = LayerMask.NameToLayer("Obstruction");
        _controller.FreezeController();
        MoveToPoint(sendToPosition);
        yield return new WaitUntil(IsNavigating);
        while (IsNavigating())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        StopAgent();
        gameObject.layer = LayerMask.NameToLayer("Default");
        _controller.UnFreezeController();
    }

    // Start is called before the first frame update
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _controller = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Controller>();
    }
    
    public void MoveToPoint(Vector3 point)
    {
        _agent.isStopped = false;
        _agent.SetDestination(point);
    }

    public bool IsNavigating()
    {
        return !(_agent.remainingDistance <= _agent.stoppingDistance);
    }

    public void StopAgent()
    {
        _patroling = false;
        _agent.isStopped = true;
        _agent.ResetPath();
    }

    // public bool HasPath()
    // {
    //     return _agent.hasPath;
    // }

    // public NavMeshPath GetPath()
    // {
    //     return _agent.path;
    // }
    // public void Follow (Ai target)
    // {
    //     if (target.HasPath())
    //     {
    //         bool b = _agent.SetPath(target.GetPath());
    //         _agent.isStopped = false;
    //     } 
            

    // }
}

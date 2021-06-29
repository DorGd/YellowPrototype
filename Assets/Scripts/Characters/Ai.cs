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
    private CharacterAnimationManager _animationManager;
    private CustomAudioSource _customAudioSource;
    private Coroutine forceMoveCoroutine = null;
    private bool forcePauseFootsteps = false;

    public bool Patroling
    {
        get { return _patroling; }
        set { _patroling = value; }
    }

    internal void ForceMoveToPoint(Vector3 sendToPosition)
    {
        if (forceMoveCoroutine != null)
            return;
        forceMoveCoroutine = StartCoroutine(ForceMoveCoroutine(sendToPosition));
    }

    private IEnumerator ForceMoveCoroutine(Vector3 sendToPosition)
    {
        yield return new WaitForEndOfFrame();
        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
        _controller.FreezeController();
        while (IsNavigating())
        {
            yield return new WaitForEndOfFrame();
        }
        MoveToPoint(sendToPosition);
        yield return new WaitUntil(IsNavigating);
        while (IsNavigating())
        {
            yield return new WaitForEndOfFrame();
        }
        gameObject.layer = LayerMask.NameToLayer("Default");
        foreach (Transform child in transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        _controller.UnFreezeController();
        forceMoveCoroutine = null;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _animationManager = GetComponentInChildren<CharacterAnimationManager>();
        _agent = GetComponent<NavMeshAgent>();
        _controller = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Controller>();
        _customAudioSource = GetComponent<CustomAudioSource>();
    }

    private void Update()
    {
        if (_animationManager == null || !_animationManager.enabled || !_agent.enabled)
            return;
        if (IsNavigating())
        {
            if (!forcePauseFootsteps) _customAudioSource?.Continue();
            _animationManager.PlayAnimation(AnimationType.Walk);
        }
        else
        {
            _customAudioSource?.Pause();
            _animationManager.PlayAnimation(AnimationType.Idle);
        }
    }

    public void PauseFootsteps()
    {
        forcePauseFootsteps = true;
        _customAudioSource?.Pause();
    }

    public void UnPauseFootsteps()
    {
        forcePauseFootsteps = false;
        if (IsNavigating())
        {
            _customAudioSource?.Continue();
        }
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

    public void OverrideForceMoveCoroutine()
    {
        if (forceMoveCoroutine != null)
        {
            StopCoroutine(forceMoveCoroutine);
            forceMoveCoroutine = null;
        }
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

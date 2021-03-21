using System;
using UnityEngine;
using UnityEngine.Events;

public class Paradigm 
{
    public UnityEvent action;
    public int startTime;
    public int endTime;
    public Transform[] patrolPath;
    public Regulation[] regulations;

    public Paradigm(UnityEvent _event, int _startTime, int _endTime , Transform[] _patrolPath, Regulation[] _regulations)
    {
        action = _event;
        startTime = _startTime;
        endTime = _endTime;
        patrolPath = _patrolPath;
        regulations = _regulations;
    }
}

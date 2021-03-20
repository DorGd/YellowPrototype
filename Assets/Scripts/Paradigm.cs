using System;
using UnityEngine.Events;

public class Paradigm 
{
    public UnityEvent action;
    public int startTime;
    public int endTime;
    //Path patrolPath;
    public Paradigm(UnityEvent _event, int _startTime, int _endTime /* , Path patrolPath */ )
    {
        action = _event;
        startTime = _startTime;
        endTime = _endTime;
        //patrolPath = _patrolPath;
    }
}

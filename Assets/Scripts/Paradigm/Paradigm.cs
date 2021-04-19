using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Paradigm", menuName = "Paradigm/Empty Paradigm", order = 1)]
public class Paradigm : ScriptableObject
{
    public EnemyAction action;
    public int startTime;
    public int endTime;
    public Transform[] patrolPath;
    public Regulation[] regulations;

    public Paradigm(EnemyAction _action, int _startTime, int _endTime , Regulation[] _regulations)
    {
        action = _action;
        startTime = _startTime;
        endTime = _endTime;
        regulations = _regulations;
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Paradigm", menuName = "Paradigm/Empty Paradigm", order = 1)]
public class ParadigmSO : ScriptableObject
{
    public ActionSO action;
    public int startTime;
    public int endTime;
    public Transform[] patrolPath;
    public HeldItemsRegulation[] regulations;

    public ParadigmSO(ActionSO _action, int _startTime, int _endTime , HeldItemsRegulation[] _regulations)
    {
        action = _action;
        startTime = _startTime;
        endTime = _endTime;
        regulations = _regulations;
    }
}

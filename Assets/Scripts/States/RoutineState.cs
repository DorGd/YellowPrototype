using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RoutineState : State
{
    [SerializeField] private RoutineState nextRoutine;
    [SerializeField] public int endTime;

    public RoutineState(StateMachine stateMachine) : base(stateMachine)
    {
    }
}

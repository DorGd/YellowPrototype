using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private RoutineState routineState;

    private State currState;

    private void Start()
    {
        currState = routineState;
    }

    private void Update()
    {
        currState = currState.doState();
    }
}

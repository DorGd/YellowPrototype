using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

[System.Serializable]
public abstract class State : MonoBehaviour
{
    protected StateMachine machine;

    public State(StateMachine stateMachine)
    {
        machine = stateMachine;
    }

    public virtual State doState()
    {
        return this;
    }

}

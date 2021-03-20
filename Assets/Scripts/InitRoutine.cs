using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InitRoutine : MonoBehaviour
{
    public UnityEvent[] Events;
    public int[] StartTime;
    public int[] EndTime;
    
    public Paradigm[] Init()
    {
        int[] arr = new int[] { Events.Length, StartTime.Length, EndTime.Length };
        int maxIdx = Mathf.Min(arr);

        Paradigm[] paradigms = new Paradigm[maxIdx];
        for (int i = 0; i < maxIdx; i++)
        {
            paradigms[i] = new Paradigm(Events[i], StartTime[i], EndTime[i]);
        }

        return paradigms;
    }

}

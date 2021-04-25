using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class Path
{
    public Transform[] pointList;
}

[System.Serializable]
public class PathList
{
    public Path[] pathList;

    public Transform[] getPath(int i)
    {
        return pathList[i].pointList;
    }
}

public class InitRoutine : MonoBehaviour
{
    public Action[] Events;
    public int[] StartTimes;
    public int[] EndTimes;
    public PathList patrolPaths;
    public RegulationsList regulations;
    
    public ParadigmSO[] Init()
    {
        int[] arr = new int[] { Events.Length, StartTimes.Length, EndTimes.Length };
        int maxIdx = Mathf.Min(arr);

        ParadigmSO[] paradigms = new ParadigmSO[maxIdx];
        //for (int i = 0; i < maxIdx; i++)
        //{
        //    paradigms[i] = new Paradigm(Events[i], StartTimes[i], EndTimes[i], patrolPaths.getPath(i), regulations.getRegulations(i));
        //}

        return paradigms;
    }

}

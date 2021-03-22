using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RegulationParadigm
{
    public Regulation[] regulations;
}

[System.Serializable]
public class RegulationsList
{
    public RegulationParadigm[] regulationsList;

    public Regulation[] getRegulations(int i)
    {
        return regulationsList[i].regulations;
    }
}

public class Regulation : MonoBehaviour
{
    public enum Severity
    {
        Low = 0,
        Medium = 1,
        High = 2
    };

    public Severity punishment;
    public bool punishedByLocation;
    public GameObject[] requiredEquipment;
    public GameObject[] forbiddenEquipment;
    public Transform spawnPosition;
    public delegate void TestDelegate(); // This defines what type of method you're going to call.
    public TestDelegate m_methodToCall;

    public Severity GetSeverity()
    {
        return punishment;
    }

    public bool isValid(GameObject[] equipment)
    {
        if (punishedByLocation)
        {
            return false;
        }
        foreach (GameObject eq in equipment)
        {
            if (eq == null)
            {
                continue;
            }
            foreach (GameObject feq in forbiddenEquipment) {
                if (feq == null)
                    continue;
                if (feq.Equals(eq) || eq.gameObject.name.StartsWith(feq.gameObject.name))
                {
                    return false;
                }
            }
        }

        bool reqFlag;
        foreach (GameObject req in requiredEquipment)
        {
            reqFlag = false;
            foreach (GameObject eq in equipment)
            {
                if (eq == null)
                    continue;
                if (req.Equals(eq) || eq.gameObject.name.StartsWith(req.gameObject.name))
                {
                    reqFlag = true;
                    break;
                }
            }
            if (!reqFlag)
            {
                return false;
            }
        }
        return true;
    }

    public GameObject[] getRequired()
    {
        return requiredEquipment;
    }

    public GameObject[] getForbidden()
    {
        return forbiddenEquipment;
    }
}

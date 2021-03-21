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
    public GameObject[] requiredEquipment;
    public GameObject[] forbiddenEquipment;

    public Severity GetSeverity()
    {
        return punishment;
    }

    public bool isValid(GameObject[] equipment)
    {
        foreach (GameObject eq in equipment)
        {
            foreach (GameObject feq in forbiddenEquipment) {
                if (feq.Equals(eq))
                {
                    return false;
                }
            }
        }

        bool reqFlag;
        foreach (GameObject req in requiredEquipment)
        {
            reqFlag = false;
            foreach (GameObject eq in requiredEquipment)
            {
                if (req.Equals(eq))
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

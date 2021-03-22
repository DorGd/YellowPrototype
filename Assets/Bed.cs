using System;
using UnityEngine;

public class Bed : MonoBehaviour, IInteractable
{
    public Action[] CalcInteractions()
    {
        int time = GameManager.Instance.Clock.GetHour();
        if (time >= 7 || (time >= 0 && time < 6))
        {
            return new Action[] { Sleep };
        }
        else return new Action[] { };
    }

    public void Sleep()
    {
        GameManager.Instance.ResetDay();
    }
}


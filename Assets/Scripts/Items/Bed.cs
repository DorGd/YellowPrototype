using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bed : Interactable
{
    [SerializeField] private SpeechTextSO cantSleepText;
    public override Action[] CalcInteractions()
    {
        return new Action[] { Sleep };
    }

    // Start is called before the first frame update
    void Sleep()
    {
        if (GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes() < 18.5)
        {
            GameManager.Instance.SpeechManager.StartSpeech(transform.position, cantSleepText);
            return;
        }
        GameManager.Instance.EndDayTransition($"End of Day {AudioManager.numDay}");
    }
}

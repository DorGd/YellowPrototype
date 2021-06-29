using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickOff : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        AudioManager.Instance.JustInitialize();
        if (AudioManager.numDay == 1)
        {
            GameManager.Instance.StartDayTransition("Day One");
        }
        else
        {
           GameManager.Instance.StartDayTransition($"Day One #{ AudioManager.numDay}"); 
        }
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickOff : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        AudioManager.Instance.JustInitialize();    
        gameObject.SetActive(false);
    }
}

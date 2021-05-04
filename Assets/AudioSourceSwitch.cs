using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSwitch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Equals("Player"))
        {
            GetComponent<AudioSource>().Pause();
        }
    }
}

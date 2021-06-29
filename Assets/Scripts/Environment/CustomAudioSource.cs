using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[ExecuteInEditMode]
public class CustomAudioSource : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    float maxRadius = 5.0f;

    [SerializeField]
    [Range(0, 10)]
    float volumeModifier = 1.0f;

    [SerializeField]
    AudioClip clip;

    [SerializeField]
    bool loop;

    private Transform player;
    private AudioManager.IAudioSourceHandler sourceHandler;
    private bool paused;
    public bool Paused { get; }

    void OnDrawGizmosSelected()
    {
        // Display the maximum hearing radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sourceHandler = null;
    }

    float CalcVolume(float distance)
    {
        return 1 - (distance / maxRadius);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
            return;
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= maxRadius)
        {
            if (sourceHandler == null)
            {
                sourceHandler = AudioManager.Instance.GetAvailableAudioSourceHandle();
                if (clip == null)
                    return;
                sourceHandler.SetClip(clip);
                sourceHandler.Play(volumeModifier * CalcVolume(distance), loop);
            }
            else
            {
                sourceHandler.SetVolume(volumeModifier * CalcVolume(distance));
            }
        }
        else if (sourceHandler != null)
        {
            sourceHandler.ReleaseHandler();
            sourceHandler = null;
        }
    }

    public void Pause()
    {
        if (sourceHandler != null && !paused)
        {
            sourceHandler.Pause();
        }
        paused = true;
    }

    public void Continue()
    {
        if (sourceHandler != null && paused)
        {
            sourceHandler.UnPause();
        }
        paused = false;
    }
}

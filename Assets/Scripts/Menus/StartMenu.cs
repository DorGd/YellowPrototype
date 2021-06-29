using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    public Image blackScreen;
    public Slider volumeSlider;
    private void Start()
    {
        AudioManager.IAudioSourceHandler musicAS = AudioManager.Instance.GetAvailableAudioSourceHandle();
        musicAS.SetClip(AudioManager.Music_motive);
        musicAS.SetLoop(true);
        musicAS.SetVolume(1f);
        musicAS.Play();
        AudioManager.IAudioSourceHandler ambienceAS = AudioManager.Instance.GetAvailableAudioSourceHandle();
        ambienceAS.SetClip(AudioManager.Ambience_warehouse);
        ambienceAS.SetLoop(true);
        ambienceAS.SetVolume(0.5f);
        ambienceAS.Play();
        StartCoroutine(FadeAway());
    }

    private IEnumerator FadeAway()
    {
        float startTime = Time.time;
        float curTime = startTime;
        yield return new WaitForSeconds(1f);
        while (curTime - startTime < 2f)
        {
            blackScreen.color -= (Time.deltaTime / 2f) * new Color(0f, 0f, 0f, 1f);
            yield return new WaitForEndOfFrame();
            curTime += Time.deltaTime;
        }
        blackScreen.color = new Color(1f, 1f, 1f, 0f);
        blackScreen.enabled = false;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Prototype");
    }
    
    public void Exit()
    {
        Application.Quit ();
    }

    public void SetVolume()
    {
        AudioManager.Instance.SetGlobalVolume(volumeSlider.value);
    }

    
}

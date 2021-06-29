using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneMenu : MonoBehaviour
{
    
    public Slider volumeSlider;
    
    public void OpenSettings()
    {
        volumeSlider.value = AudioManager.Instance.GlobalVolume;
    }
    public void CloseSettings()
    {
        GameManager.Instance.Resume();
    }
    
    public void Exit()
    {
        GameManager.Instance.Exit();
    }

    public void SetVolume()
    {
        AudioManager.Instance.SetGlobalVolume(volumeSlider.value);
    }
}

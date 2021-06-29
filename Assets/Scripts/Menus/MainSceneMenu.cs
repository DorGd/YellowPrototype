using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneMenu : MonoBehaviour
{
    
    public GameObject settingPanel;


    public void OpenSettings()
    {
        settingPanel.SetActive(true);
    }
    
    public void CloseSettings()
    {
        Time.timeScale = 1f;
        settingPanel.SetActive(false);
    }
    
    public void Exit()
    {
        Application.Quit ();
    }
}

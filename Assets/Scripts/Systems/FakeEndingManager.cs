using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FakeEndingManager : MonoBehaviour
{
    public Image blackScreen;
    // Start is called before the first frame update
    private void Start()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Music_motiveReprise);
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

    public void Exit()
    {
        Application.Quit();
    }
}

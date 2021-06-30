using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FakeEndingManager : MonoBehaviour
{
    public Image blackScreen;
    public RectTransform credits;
    public GameObject mainMenuButton;

    // Start is called before the first frame update
    private void Start()
    {
        AudioManager.Instance.PlayOneShot(AudioManager.Music_motiveReprise);
        StartCoroutine(FadeAway());
        StartCoroutine(Scroll());
        StartCoroutine(FadeMainMenuButton());
    }

    private IEnumerator FadeMainMenuButton()
    {
        mainMenuButton.SetActive(false);
        float startTime = Time.time;
        float curTime = startTime;
        yield return new WaitForSeconds(13f);
        mainMenuButton.SetActive(true);
    }

    private IEnumerator Scroll()
    {
        float startTime = Time.time;
        float curTime = startTime;
        yield return new WaitForSeconds(2f);
        while (curTime - startTime < 15f)
        {
            credits.anchoredPosition += (Time.deltaTime / 15f) * Vector2.up * 950f;
            yield return new WaitForEndOfFrame();
            curTime += Time.deltaTime;
        }
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
        blackScreen.color = new Color(0f, 0f, 0f, 0f);
        blackScreen.enabled = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }
}

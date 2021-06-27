using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bed : Interactable
{
    [SerializeField] private float _blackOutDuration;
    [SerializeField] private float _textDuration;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _bg;
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
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        _text.text = $"End of Day {AudioManager.numDay}";
        float elapsedTime = 0f;
        Color colorToAdd = new Color(0f, 0f, 0f, 0f);
        while (elapsedTime < _blackOutDuration)
        {
            elapsedTime += Time.deltaTime;
            colorToAdd.a = Mathf.Lerp(0f, 1f, elapsedTime / _blackOutDuration) - _bg.color.a;
            _bg.color += colorToAdd;
            yield return null;
        }

        colorToAdd.a = 0f;
        elapsedTime = 0f;
        while (elapsedTime < _textDuration)
        {
            elapsedTime += Time.deltaTime;
            colorToAdd.a = Mathf.Lerp(0f, 1f, elapsedTime / _blackOutDuration) - _text.color.a;
            _text.color += colorToAdd;
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        AudioManager.numDay += 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

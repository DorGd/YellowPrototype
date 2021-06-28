using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private float _blackOutDuration;
    [SerializeField] private float _textDuration;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _bg;
    public IEnumerator EndDayTransition(string text)
    {
        _text.text = text;
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

    public IEnumerator StartDayTransition(string text)
    {
        _text.text = text;
        _bg.color += new Color(0f, 0f, 0f, 1f);
        _text.color += new Color(0f, 0f, 0f, 1f);
        float elapsedTime = 0f;
        Color colorToAdd = new Color(0f, 0f, 0f, 0f);
        yield return new WaitForSeconds(_textDuration);
        while (elapsedTime < _blackOutDuration)
        {
            elapsedTime += Time.deltaTime;
            colorToAdd.a = Mathf.Lerp(0f, -1f, elapsedTime / _blackOutDuration) + (1 - _bg.color.a);
            _bg.color += colorToAdd;
            _text.color += colorToAdd;
            yield return null;
        }
    }
}

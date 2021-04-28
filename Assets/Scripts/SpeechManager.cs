using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechManager : MonoBehaviour
{
    [Header("GUI")]
    public Canvas speechCanvas;
    public Image bubbleImage;
    public TextMeshProUGUI speechTextBox;

    [Header("Bubble Position")]
    public float xFromSource = 10;
    public float yFromSource = 10;

    [Header("Button")]
    public GameObject advanceButton;
    public TextMeshProUGUI buttonText;
    public Animator bubbleAnimator;
    public float typingDelay = 0.01f;

    private bool isAvailable;
    private Vector3 speechSource;
    private string[] currentSentences;
    private int sentenceIndex;
    private float scaleDelay = 0.5f;


    void Start()
    {
        isAvailable = true;
        speechSource = Vector3.zero;
        sentenceIndex = 0;
        if (advanceButton)
        {
            advanceButton.SetActive(false);
        }
    }

    private void Update()
    {
        //if (!isAvailable && speechSource != null)
        //{
        //    DrawTail();
        //}
    }

    private void SetBubblePosition()
    {
        float bubbleWidth = bubbleImage.rectTransform.rect.width * speechCanvas.scaleFactor;
        float bubbleHeight = bubbleImage.rectTransform.rect.height * speechCanvas.scaleFactor;
        float xPos, yPos;

        if (Screen.width - speechSource.x < bubbleWidth + xFromSource)
        {
            xPos = speechSource.x - ((bubbleWidth / 2) + xFromSource);
        }
        else
        {
            xPos = (bubbleWidth / 2) + speechSource.x + xFromSource;
        }

        if (Screen.height - speechSource.y < bubbleHeight + yFromSource)
        {
            yPos = speechSource.y - ((bubbleHeight / 2) + yFromSource);
        }
        else
        {
            yPos = (bubbleHeight / 2) + speechSource.y + yFromSource;
        }

        bubbleImage.transform.position = new Vector3(xPos, yPos, 0);
    }

    public void startSpeech(GameObject speaker, SpeechText text)
    {
        if (isAvailable)
        {
            isAvailable = false;
            speechCanvas.gameObject.SetActive(true);
            speechSource = Camera.main.WorldToScreenPoint(speaker.transform.position);
            SetBubblePosition();
            currentSentences = text.sentences;
            sentenceIndex = 0;
            speechTextBox.text = "";
            advanceButton.SetActive(false);
            Time.timeScale = 0f;
            StartCoroutine(openSpeech());
        }
    }

    public void advanceSpeech()
    {
        if (sentenceIndex < currentSentences.Length)
        {
            speechTextBox.text = "";
            StartCoroutine(typeText());
        }
        else
        {
            StartCoroutine(closeSpeech());
        }
    }

    private IEnumerator openSpeech()
    {
        bubbleAnimator.SetTrigger("Open");
        yield return new WaitForSecondsRealtime(scaleDelay);
        StartCoroutine(typeText());
    }

    private IEnumerator typeText()
    {
        if (sentenceIndex < currentSentences.Length) {
            advanceButton.SetActive(false);
            foreach (char letter in currentSentences[sentenceIndex].ToCharArray())
            {
                speechTextBox.text += letter;
                yield return new WaitForSecondsRealtime(typingDelay);
            }
            sentenceIndex++;
            buttonText.text = (sentenceIndex < currentSentences.Length) ? "Next" : "End";
            advanceButton.SetActive(true);
        }
    }

    private IEnumerator closeSpeech()
    {
        bubbleAnimator.SetTrigger("Close");
        yield return new WaitForSecondsRealtime(scaleDelay);
        Time.timeScale = 1f;
        isAvailable = true;
        speechSource = Vector3.zero;
        currentSentences = null;
        sentenceIndex = 0;
        speechTextBox.text = "";
        advanceButton.SetActive(false);
        speechCanvas.gameObject.SetActive(false);
    }

    //private void DrawTail()
    //{
    //    //Vector3 tailStart = bubbleImage.rectTransform
    //    //tailMesh.vertices = [speechSource.transform.position, ];
    //}
}

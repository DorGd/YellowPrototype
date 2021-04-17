using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeechManager : MonoBehaviour
{
    [Header("GUI")]
    public GameObject speechCanvas;
    public Image bubbleImage;
    public TextMeshProUGUI speechTextBox;

    MeshFilter speechTailMeshFilter;
    private Mesh tailMesh;

    [Header("Button")]
    public GameObject advanceButton;
    public TextMeshProUGUI buttonText;
    public Animator bubbleAnimator;
    public float typingDelay = 0.01f;

    private bool isAvailable;
    private GameObject speechSource;
    private string[] currentSentences;
    private int sentenceIndex;
    private float scaleDelay = 0.5f;


    void Start()
    {
        isAvailable = true;
        tailMesh = new Mesh();
        tailMesh.name = "Field Mesh";
        speechTailMeshFilter.mesh = tailMesh;
        speechSource = null;
        sentenceIndex = 0;
        advanceButton.SetActive(false);
    }

    private void Update()
    {
        if (!isAvailable && speechSource != null)
        {
            DrawTail();
        }
    }

    public void startSpeech(GameObject speaker, string[] stentences)
    {
        if (isAvailable)
        {
            isAvailable = false;
            speechCanvas.SetActive(true);
            speechSource = speaker;
            currentSentences = stentences;
            sentenceIndex = 0;
            speechTextBox.text = "";
            advanceButton.SetActive(false);
            Time.timeScale = 0f;
            StartCoroutine(openSpeech());
        }
    }

    public void sayHi()
    {
        if (isAvailable)
        {
            isAvailable = false;
            speechCanvas.SetActive(true);
            currentSentences = new string[2]{ "Hello, I'm...", "Your father"};
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
        tailMesh.Clear();
        speechSource = null;
        currentSentences = null;
        sentenceIndex = 0;
        speechTextBox.text = "";
        advanceButton.SetActive(false);
        speechCanvas.SetActive(false);
    }

    private void DrawTail()
    {
        //Vector3 tailStart = bubbleImage.rectTransform
        //tailMesh.vertices = [speechSource.transform.position, ];
    }
}

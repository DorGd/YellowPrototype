using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private MainInventory _inventory;
    private Clock _clock;
    private Controller _controller;
    private SpeechManager _speechManager;
    private bool paused = false;
    private bool _isHighlightInteractables = false;
    private TransitionManager _transitionManager;
    private MainSceneMenu _pauseMenu;

    public MainInventory inventory
    {
        get { return _inventory; }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public void RestartDay()
    {
        SceneManager.LoadScene("Prototype");
    }

    public  Clock Clock 
    { 
        get { return _clock; } 
    }

    private Transform _playerTransform;
    public Transform PlayerTransform
    {
        get { return _playerTransform; }
    }

    private Ai _playerAi;
    public Ai PlayerAI
    {
        get {return _playerAi; }
    }
    public SpeechManager SpeechManager
    {
        get { return _speechManager; }
    }

    public bool IsHighlightInteractables { get { return _isHighlightInteractables; }  }

    public void EndDayTransition(string text)
    {
        StopSkip();
        _controller.FreezePauseMenu();
        _speechManager.Refuse();
        StartCoroutine(_transitionManager.EndDayTransition(text));
    }

    internal void EndLevel()
    {
        _controller.FreezePauseMenu();
        _speechManager.Refuse();
        StartCoroutine(_transitionManager.EndGameTransition());
    }

    public void StartDayTransition(string text)
    {
        StartCoroutine(_transitionManager.StartDayTransition(text));
    }

    public event Action onShockTransition;
    
    //Constructor
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<GameManager>();
                    Debug.Log("Creating new GameManager");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _clock = GetComponent<Clock>();
        _inventory = GetComponent<MainInventory>();
        _inventory.SetMainInventory();
        _speechManager = GetComponent<SpeechManager>();
        _playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        _playerAi = _playerTransform.GetComponent<Ai>();
        _controller = _playerTransform.GetComponent<Controller>();
        _transitionManager = GetComponent<TransitionManager>();
        _pauseMenu = GameObject.Find("PauseMenuCanvas").GetComponent<MainSceneMenu>();
        _pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void InvokeShock()
    {
        onShockTransition?.Invoke();
    }

    public void Skip()
    {
        //float time = Instance.Clock.GetHour() + Instance.Clock.GetMinutes();
        //if ((time > 6f && time < 7.5f) || (time > 8.5f && time < 18.5f))
        //{
        //    _clock.REAL_SECONDS_PER_INGAME_DAY = 100f;
        //}
        if (paused)
            StopTime();
        else
            Time.timeScale = 5.5f;
    }

    public void StopSkip()
    {
        if (paused)
            StopTime();
        else
            Time.timeScale = 1f;
    }

    public void StopTime()
    {
        paused = true;
        Ai[] ais = GameObject.FindObjectsOfType<Ai>();
        foreach (Ai ai in ais)
        {
            ai.PauseFootsteps();
        }
        Time.timeScale = 0f;
    }

    public void ContinueTime()
    {
        Ai[] ais = GameObject.FindObjectsOfType<Ai>();
        foreach (Ai ai in ais)
        {
            ai.UnPauseFootsteps();
        }
        Time.timeScale = 1f;
        paused = false;
    }

    public void HighlightInteractables(bool enabled)
    {
        _isHighlightInteractables = enabled;
        foreach (Interactable interactable in GameObject.FindObjectsOfType<Interactable>())
        {
            if (interactable.MouseOver)
                continue;
            interactable.GetComponent<Outline>().enabled = enabled;
        }
    }

    public void PauseMenu()
    {
        if (_pauseMenu.gameObject.activeInHierarchy)
        {
            Resume();
            return;
        }
        _pauseMenu.OpenSettings();
        _pauseMenu.gameObject.SetActive(true);
        StopTime();
    }

    public void Resume()
    {
        _pauseMenu.gameObject.SetActive(false);
        ContinueTime();
    }

    public void Exit()
    {
        Application.Quit();
    }
}

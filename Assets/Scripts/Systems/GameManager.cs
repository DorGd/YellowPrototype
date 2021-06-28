using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private MainInventory _inventory;
    private Clock _clock;
    private SpeechManager _speechManager;
    private TransitionManager _transitionManager;
    public MainInventory inventory
    {
        get { return _inventory; }
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

    public void EndDayTransition(string text)
    {
        StartCoroutine(_transitionManager.EndDayTransition(text));
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
        _transitionManager = GetComponent<TransitionManager>();
    }

    public void InvokeShock()
    {
        onShockTransition?.Invoke();
    }

}

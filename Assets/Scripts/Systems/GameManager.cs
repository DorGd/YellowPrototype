using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    private Inventory _inventory;
    private Clock _clock;
    private SpeechManager _speechManager;
    public Inventory inventory
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
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            DontDestroyOnLoad(this);
        }
        else
        {
            _clock = GetComponent<Clock>();
            _inventory = GetComponent<Inventory>();
            _inventory.SetMainInventory();
            _speechManager = GetComponent<SpeechManager>();
            _playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
            _playerAi = _playerTransform.GetComponent<Ai>();
        }
    }

}

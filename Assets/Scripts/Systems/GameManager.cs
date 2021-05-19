using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    private MainInventory _inventory;
    private Clock _clock;
    private Transform _playerTransform;
    private SpeechManager _speechManager;
    public Inventory inventory
    {
        get { return _inventory; }
    }

    public  Clock Clock 
    { 
        get { return _clock; } 
    }

    public Transform PlayerTransform
    {
        get { return _playerTransform; }
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
            _inventory = GetComponent<MainInventory>();
            _inventory.SetMainInventory();
            _speechManager = GetComponent<SpeechManager>();
            _playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
    }

}

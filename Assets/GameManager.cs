using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;


    private Inventory _inventory;
    private Clock _clock;
    private Transform _playerTransform;
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
            _playerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        }
    }

}

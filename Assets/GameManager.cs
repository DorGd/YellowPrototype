using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    //private Inventory _inventory;
    private Clock _clock;
    /*public Inventory Inventory
    {
        get { return _inventory; }
    }*/
    public  Clock Clock 
    { 
        get { return _clock; } 
    }
    
    public Inventory inventory;
    
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
            inventory = GetComponent<Inventory>();
        }
    }

    private void Start()
    {
        inventory = gameObject.AddComponent<Inventory>();
        inventory.SetMainInventory();
    }
}

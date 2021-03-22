using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;


    private Inventory _inventory;
    private Clock _clock;
    private Dictionary<GameObject, Vector3> objectsOriginalPositions = new Dictionary<GameObject, Vector3>();
    private Vector3 playerStartPos;
    public Inventory Inventory
    {
        get { return _inventory; }
    }

    public  Clock Clock 
    { 
        get { return _clock; } 
    }
    
    public Inventory inventory;
    public Transform objects;
    public GameObject diningRoom;
    public GameObject miningFacility;
    public GameObject room;

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
        playerStartPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        foreach( Transform child in objects)
        {
            objectsOriginalPositions.Add(child.gameObject, child.position);
        }
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

    public void Confiscate(GameObject item)
    {
        Vector3 pos = Vector3.zero;
        objectsOriginalPositions.TryGetValue(item, out pos);
        if (pos != Vector3.zero)
        {
            if (item.name.Equals("SecurityBadge"))
            {
                GameObject.FindGameObjectWithTag("Player").transform.Find("Badge").gameObject.SetActive(false);
            }
            item.transform.position = pos;
            item.SetActive(true);
            if (_inventory.IsInInventory(item, false))
            {
                _inventory.RemoveItem(item);
                Inventory insideInventory = item.GetComponent<Inventory>();
                if (insideInventory != null)
                {
                    foreach (GameObject insideItem in insideInventory.inventoryItems)
                    {
                        objectsOriginalPositions.TryGetValue(insideItem, out pos);
                        if (pos != Vector3.zero)
                        {
                            item.transform.position = pos;
                            item.SetActive(true);
                            if (_inventory.IsInInventory(item, false))
                            {
                                _inventory.RemoveItem(item);
                            }
                        }
                    }
                }
            }
        }
    }

    public void ResetDay()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = playerStartPos;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().MoveToPoint(playerStartPos);
        diningRoom.SetActive(false);
        miningFacility.SetActive(false);
        room.SetActive(true);
        Clock.ResetDay();
    }

    private void Start()
    {
        inventory = gameObject.AddComponent<Inventory>();
        inventory.SetMainInventory();
        _inventory = inventory;
    }
}

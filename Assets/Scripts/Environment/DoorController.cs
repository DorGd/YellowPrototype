using System;
using UnityEngine;

public class DoorController : Interactable
{

    public ItemType keyType;
    private Animator _animator;
    private int originalLayer;

    public override Action[] CalcInteractions()
    {
        return new Action[] {Open };
    }

    /** stayOpen/Close uses to enforce door state from the RoutineManager **/
    public bool StayOpen { get; set;}
    public bool StayClose { get; set;}

    void Awake()
    {
        originalLayer = gameObject.layer;
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC") && !StayClose)
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!StayOpen)
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (_animator != null)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            _animator.SetBool("Open", true);
        }
    }

    public void CloseDoor()
    {
        if (_animator != null)
        {
            gameObject.layer = originalLayer;
            _animator.SetBool("Open", false);
        }
    }

    public void Open()
    {
        if (GameManager.Instance.inventory.IsInInventory(keyType))
        {
            OpenDoor();
        }
    }
}

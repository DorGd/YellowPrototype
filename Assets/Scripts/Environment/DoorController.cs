using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator mAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("npc") || other.gameObject.CompareTag("Player"))
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("npc") || other.gameObject.CompareTag("Player"))
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (mAnimator != null)
        {
            mAnimator.SetBool("Open", true);
        }
    }

    public void CloseDoor()
    {
        if (mAnimator != null)
        {
            mAnimator.SetBool("Open", false);
        }
    }
}

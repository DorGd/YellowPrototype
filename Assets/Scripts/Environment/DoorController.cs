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
<<<<<<< HEAD
        if (other.gameObject.CompareTag("npc") || other.gameObject.CompareTag("Player"))
=======
        if (other.gameObject.CompareTag("NPC"))
>>>>>>> main
        {
            OpenDoor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
<<<<<<< HEAD
        if (other.gameObject.CompareTag("npc") || other.gameObject.CompareTag("Player"))
=======
        if (other.gameObject.CompareTag("NPC"))
>>>>>>> main
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

using UnityEngine;

public class DoorController : MonoBehaviour
{
    private Animator _animator;

    /** stayOpen/Close uses to enforce door state from the RoutineManager **/
    public bool StayOpen { get; set;}
    public bool StayClose { get; set;}

    void Awake()
    {
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
        if (other.gameObject.CompareTag("NPC") && !StayOpen)
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        if (_animator != null)
        {
            _animator.SetBool("Open", true);
        }
    }

    public void CloseDoor()
    {
        if (_animator != null)
        {
            _animator.SetBool("Open", false);
        }
    }
}

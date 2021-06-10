using UnityEngine;
using UnityEngine.Events;

public class ColliderBasedEvent : MonoBehaviour
{
    public bool isOneTimeEvent = true;
    public UnityEvent collisionEvent;
    
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collisionEvent?.Invoke();
            if (isOneTimeEvent) gameObject.SetActive(false);
        }
    }
}

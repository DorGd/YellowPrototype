using UnityEngine.Events;
using UnityEngine;

public class TestEvent : MonoBehaviour
{
    // Class declaration
    [System.Serializable]
    public class EnemyEvent : UnityEvent<EnemyManager> {}
    
    
    // Somewhere in a component
    // Declare
    public EnemyEvent Event;
    public EnemyManager enemy;
    
    public void ActivateEnemyEvent()
    {
        Event.Invoke(enemy);
    }
}

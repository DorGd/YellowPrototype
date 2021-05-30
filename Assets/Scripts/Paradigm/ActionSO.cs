using UnityEngine;

public abstract class ActionSO : ScriptableObject
{
    public abstract Coroutine Act(EnemyManager enemy);

}

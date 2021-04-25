using UnityEngine;

public abstract class ActionSO : ScriptableObject
{
    public abstract void Act(EnemyManger enemy);
}

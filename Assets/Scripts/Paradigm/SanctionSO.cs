using UnityEngine;

public abstract class SanctionSO : ScriptableObject
{
    public abstract void Apply(EnemyManager enemy);
}

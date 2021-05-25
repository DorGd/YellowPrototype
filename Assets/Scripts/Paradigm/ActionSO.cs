using UnityEngine;
using UnityEngineInternal;
public abstract class ActionSO : ScriptableObject
{
    public abstract Coroutine Act(EnemyManager enemy);

    // public void Act(string enemyName)
    // {
    //     EnemyManager enemy = GameObject.Find(enemyName).GetComponent<EnemyManager>();
    //     enemy.CurrentCoroutine = Act(enemy);
    // }
}

using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Empty")]

public class EmptyAction : ActionSO
{
    public override Coroutine Act(EnemyManager enemy)
    {
        return enemy.StartCoroutine(EmptyCo());
    }

    public IEnumerator EmptyCo()
    {
        yield return null;
    }
}

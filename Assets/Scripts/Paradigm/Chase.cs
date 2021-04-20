using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Chase")]
public class Chase : EnemyAction
{
    public override void Act(EnemyManger enemy)
    {
        enemy.StartCoroutine(ChaseRoutine(enemy));

    }

    private IEnumerator ChaseRoutine(EnemyManger enemy)
    {
        float dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
        while (dist > 2f)
        {
            dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
            enemy.Ai.MoveToPoint(GameManager.Instance.PlayerTransform.position);
            yield return null;
        }
        yield return null;
    }
}

using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Chase")]
public class Chase : ActionSO
{
    public override void Act(EnemyManger enemy)
    {
        enemy.StartCoroutine(ChaseRoutine(enemy));

    }

    private IEnumerator ChaseRoutine(EnemyManger enemy)
    {
        float dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
        while (dist > 3f)
        {
            dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
            enemy.Ai.MoveToPoint(GameManager.Instance.PlayerTransform.position);
            yield return null;
        }
        enemy.Ai.MoveToPoint(enemy.transform.position + (GameManager.Instance.PlayerTransform.position - enemy.transform.position) / 2);
        yield return null;
    }
}

using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Chase")]
public class Chase : ActionSO
{
    public override Coroutine Act(EnemyManager enemy)
    {
        return enemy.StartCoroutine(ChaseRoutine(enemy));
    }

    private IEnumerator ChaseRoutine(EnemyManager enemy)
    {
        float dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
        while (dist > 3f)
        {
            dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
            enemy.Ai.MoveToPoint(GameManager.Instance.PlayerTransform.position);
            yield return new WaitForEndOfFrame();
        }
        enemy.Ai.MoveToPoint(enemy.transform.position + (GameManager.Instance.PlayerTransform.position - enemy.transform.position) / 2);
        while (enemy.Ai.IsNavigating()) yield return new WaitForEndOfFrame();
        enemy.ActivateNextParadigm();
    }
}

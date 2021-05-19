using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Announce")]
public class Announce : ActionSO
{
    public override Coroutine Act(EnemyManager enemy)
    {
        return enemy.StartCoroutine(AnnounceRoutine(enemy));
    }

    IEnumerator AnnounceRoutine(EnemyManager enemy)
    {
        if (enemy.CurrentParadigm.goToPosition != null)
        {
            enemy.Ai.MoveToPoint(enemy.CurrentParadigm.goToPosition);
            yield return null;
        }
        while (enemy.Ai.IsNavigating())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameManager.Instance.SpeechManager.StartSpeech(enemy.transform.position,enemy.CurrentParadigm.text);
        yield return new WaitForSeconds(Time.deltaTime);
        enemy.ActivateNextParadigm();
    }
 
}

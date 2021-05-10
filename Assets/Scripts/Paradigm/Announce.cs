using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Announce")]
public class Announce : ActionSO
{
    public override void Act(EnemyManager enemy)
    {
        enemy.StartCoroutine(AnnounceRoutine(enemy));
    }

    IEnumerator AnnounceRoutine(EnemyManager enemy)
    {
        if (enemy.GetCurrentParadigm().goToPosition != null)
        {
            enemy.Ai.MoveToPoint(enemy.GetCurrentParadigm().goToPosition);
            yield return null;
        }
        while (enemy.Ai.IsNavigating())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameManager.Instance.SpeechManager.StartSpeech(enemy.transform.position,enemy.GetCurrentParadigm().text);
        yield return new WaitForSeconds(Time.deltaTime);
        enemy.UpdateParadigm();
        yield return null;
    }
 
}

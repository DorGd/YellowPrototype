using System.Collections;
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
        // Navigate to a go-to position if exist in the wraping ParadigmSO
        if (enemy.CurrentParadigm.goToPosition != null)
        {
            enemy.Ai.MoveToPoint(enemy.CurrentParadigm.goToPosition);
            yield return null;
        }
        while (enemy.Ai.IsNavigating())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // Look at the player and start the announce
        enemy.transform.LookAt(GameManager.Instance.PlayerTransform); // TODO make the transition gradual
        GameManager.Instance.SpeechManager.StartSpeech(enemy.transform.position,enemy.CurrentParadigm.text);
        yield return new WaitForSeconds(Time.deltaTime);
        enemy.ActivateNextParadigm();
    }
 
}
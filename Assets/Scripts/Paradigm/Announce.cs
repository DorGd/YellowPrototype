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

        // Look at the right direction and start the announce
        // TODO make the transition gradual
        if (enemy.CurrentParadigm.lookAtAngle == 0f) enemy.transform.LookAt(GameManager.Instance.PlayerTransform); 
        else enemy.transform.eulerAngles = Vector3.up * enemy.CurrentParadigm.lookAtAngle;

        GameManager.Instance.SpeechManager.StartSpeech(enemy.transform.position,enemy.CurrentParadigm.text);

        yield return new WaitForSeconds(Time.deltaTime);
        if (enemy.CurrentParadigm.sendPlayer)
        {
            GameManager.Instance.PlayerAI.ForceMoveToPoint(enemy.CurrentParadigm.sendToPosition);
        }
        enemy.ActivateNextParadigm();
    }
 
}

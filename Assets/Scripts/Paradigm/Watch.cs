using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Watch")]
public class Watch : ActionSO
{
    public float rotateSpeed = 20f;
    public override Coroutine Act(EnemyManager enemy)
    {
       return enemy.StartCoroutine(WatchRoutine(enemy));
    }

    IEnumerator WatchRoutine(EnemyManager enemy)
    {
         if (enemy.CurrentParadigm.goToPosition == null)
        {
            Debug.Log($"Gurd {enemy.name} missing go-to point for Watch action");
        }
        else
        {
            enemy.Ai.MoveToPoint(enemy.CurrentParadigm.goToPosition);
            yield return null;
        }
        
        while (enemy.Ai.IsNavigating()) {yield return new WaitForEndOfFrame(); }

        if (enemy.CurrentParadigm.watchSector == null)
        {
            Debug.Log($"Gurd {enemy.name} missing sector for Watch action");
        }
        else
        {
            Quaternion currentRotation = enemy.transform.rotation;
            Quaternion[] wantedRotation = new Quaternion[2];
            Vector2 sector = enemy.CurrentParadigm.watchSector;
            wantedRotation[0] = Quaternion.Euler(0f, sector[0] ,0f);
            wantedRotation[1] = Quaternion.Euler(0f, sector[1] ,0f);
            int i = 0;
            
            enemy.transform.rotation = wantedRotation[1];
            enemy.Ai.Patroling = true;
            while (enemy.Ai.Patroling)
            {
                currentRotation = enemy.transform.rotation;
                if (wantedRotation[i] == currentRotation) i = (i + 1) % 2;
                enemy.transform.rotation = Quaternion.RotateTowards(currentRotation, wantedRotation[i] , Time.deltaTime * rotateSpeed);
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
    }
}

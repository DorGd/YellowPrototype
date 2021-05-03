using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Announce")]
public class Announce : ActionSO
{
    public SpeechTextSO text;
    public override void Act(EnemyManager enemy)
    {
        GameManager.Instance.SpeechManager.StartSpeech(enemy.transform.position,text);
    }

    IEnumerator AnnounceRoutine(EnemyManager enemy)
    {
        yield return new WaitForSeconds(0.01f);
        Debug.Log("Announce!!!!!!!!!");
    }
    
    

}

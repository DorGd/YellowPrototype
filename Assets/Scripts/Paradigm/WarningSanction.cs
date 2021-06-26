using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Sanctions/Warning")]
public class WarningSanction : SanctionSO
{
        
    public override void Apply(EnemyManager enemy)
    {
        GameManager.Instance.SpeechManager.StartSpeech(enemy.transform.position ,enemy.CurrentParadigm.text);
        GameManager.Instance.PlayerAI.ForceMoveToPoint(enemy.CurrentParadigm.sendToPosition);
    }
}

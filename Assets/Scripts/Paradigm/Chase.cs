using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Paradigm/Actions/Chase")]
public class Chase : EnemyAction
{
    public override void Act(EnemyManger enemy)
    {
        float dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
        enemy.Ai.MoveToPoint(GameManager.Instance.PlayerTransform.position);
        
    }

}

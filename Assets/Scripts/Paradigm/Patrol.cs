using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[CreateAssetMenu(menuName = "Paradigm/Actions/Patrol")]
public class Patrol : ActionSO
{
    public override void Act(EnemyManager enemy)
    {
        enemy.StartCoroutine(PatrolRoutine(enemy));
    }
    
    IEnumerator PatrolRoutine(EnemyManager enemy)
    {
        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        int destPoint = 0;
        enemy.Ai.Patroling = true;
        Transform[] points = enemy.Ai.WayPoints;

        while (enemy.Ai.Patroling)
        {
            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                // Returns if no points have been set up
                if (points.Length == 0)
                    yield break;

                // Set the agent to go to the currently selected destination.
                agent.destination = points[destPoint].position;

                // Choose the next point in the array as the destination,
                // cycling to the start if necessary.
                destPoint = (destPoint + 1) % points.Length;
            }

            yield return null;
        }
        yield break;
    }

}

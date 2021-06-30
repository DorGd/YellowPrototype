using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Paradigm/Sanctions/Shock")]
public class ShockSanction : SanctionSO
{
    public override void Apply(EnemyManager enemy)
    {
        enemy.StartCoroutine(ShockRoutine(enemy));
        
    }

    public IEnumerator ShockRoutine(EnemyManager enemy)
    {
        AudioManager.Instance.PlayOneShot(AudioManager.SFX_gotCaughtBass);
        AudioManager.IAudioSourceHandler ash = AudioManager.Instance.GetAvailableAudioSourceHandle();
        ash.SetClip(AudioManager.Music_chase);
        ash.SetLoop(true);
        ash.Play();
        enemy.PauseAgentRoutine();
         // Navigate to a go-to position if exist in the wraping ParadigmSO
        enemy.Ai.MoveToPoint(GameManager.Instance.PlayerTransform.position);
        enemy.GetComponent<NavMeshAgent>().speed *= 1.2f;

        float dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
        while (dist > 2f)
        {
            if (dist > 20f)
            {
                ash.Fade();
                ash.ReleaseHandler();
                yield break;
                
            }
            dist = Vector3.Distance(GameManager.Instance.PlayerTransform.position, enemy.transform.position);
            enemy.Ai.MoveToPoint(GameManager.Instance.PlayerTransform.position);
            yield return new WaitForEndOfFrame();
        }
        GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Controller>().FreezeController();
        ash.Fade();
        ash.ReleaseHandler();
        AudioManager.Instance.PlayOneShot(AudioManager.SFX_hitGasp);
        // Initiate transition and reset the scene
        GameManager.Instance.EndDayTransition("Oopsie, guess you fell and bumped your head!");
    }
}

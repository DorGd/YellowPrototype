using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RoutineManager : MonoBehaviour
{
    private Dictionary<string, EnemyManager> _enemies;
    private Controller _controller;
    //public string playerCurrRoom;
    void Start()
    {
        _enemies = new Dictionary<string, EnemyManager>();
        GameManager.Instance.Clock.TickEvent += UpdateRoutine;
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        _controller = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Controller>();
        foreach(var enemy in enemies) _enemies.Add(enemy.name, enemy);
    }

    void UpdateRoutine()
    {
        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        // TODO: room collider which tell the routine manager where is the player. 
        // if (time == 8f) StartCoroutine(LaunchConvoy( new Vector3(1f,1f,1f), new Vector3(-61.6f,0f,-9.4f),"Cell Gurd 1", "Cell Gurd 2"));
    }

    public void StartTestCo()
    {
        StartCoroutine(Test());
    }

    IEnumerator Test()
    {
        ParadigmSO[] eventParadigms = new ParadigmSO[2];
        eventParadigms[0] = Resources.Load<ParadigmSO>("Routine/Paradigms/Paradigm Test 1");
        eventParadigms[1] = Resources.Load<ParadigmSO>("Routine/Paradigms/Paradigm Test 2");

        EnemyManager enemy = GameObject.Find("Cell Gurd 1").GetComponent<EnemyManager>();
        enemy.LoadEventParadigms(eventParadigms);
        enemy.InvokeEventParadigm();
        yield return null;
    }

    IEnumerator LaunchConvoy(Vector3 startPos, Vector3 endPos, Vector3 tailStartDirection ,string leadGurdName, string backGurdName)
    {
        Debug.Log("Convoy Time!");
        float offset = 4f;
        EnemyManager leadGurd = _enemies[leadGurdName];
        EnemyManager backGurd = _enemies[backGurdName];
        Ai player = GameManager.Instance.PlayerAI;

        _controller.FreezeController();
        leadGurd.StopAction();
        leadGurd.Ai.MoveToPoint(startPos);
        backGurd.StopAction();
        backGurd.Ai.MoveToPoint(startPos + tailStartDirection.normalized * offset);

        while (leadGurd.Ai.IsNavigating() && backGurd.Ai.IsNavigating())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }

        leadGurd.Ai.MoveToPoint(endPos);

        // Get First Gurd to head marker
        // Get all others + player to go in line
        // Get back gurd in place
        // Move as a convoy
        // Spread out and active the nexet paradigm

        _controller.UnFreezeController();
        yield return null;

    }
}

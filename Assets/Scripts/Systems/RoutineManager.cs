using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RoutineManager : MonoBehaviour
{
    private Dictionary<string, EnemyManager> _enemies;
    private Dictionary<string, DoorController> _doors;
    private Controller _controller;
    //public string playerCurrRoom;
    void Start()
    {
        _enemies = new Dictionary<string, EnemyManager>();
        _doors = new Dictionary<string, DoorController>();
        GameManager.Instance.Clock.TickEvent += UpdateRoutine;
        _controller = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Controller>();
        DoorController[] doors = FindObjectsOfType<DoorController>();
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        foreach(var enemy in enemies) _enemies.Add(enemy.name, enemy);
        foreach(var door in doors) _doors.Add(door.name, door);
    }

    void UpdateRoutine()
    {
        // this section contain all the conditions which triggers the ROUTINE MANAGER events.
        // those events are characterized by main factory events that include the synchronization of multiple agents.
        //
        // example:
        // if (player in factory and time = 18) => Convoy(factory);
        // if (player in leaving room and time = 6) => Breakfast();
        //
        // etc...

        // TODO: add room colliders which tells the routine manager where is the player (playerCurrRoom variable). 

        float time = GameManager.Instance.Clock.GetHour() + GameManager.Instance.Clock.GetMinutes();
        string[] names = {"Leaving Room Guard 1", "Leaving Room Guard 2"}; 
        string[] doors = {"PlayerRoomDoor", "MiningFacilityDoor"};
        DoorController cellDoor = GetDoorsByName(doors)[0];
        DoorController factoryDoor = GetDoorsByName(doors)[1];
        EnemyManager[] enemies = GetAgentsByName(names);

        /** 0600 **/
        if (time == 6f) cellDoor.StayOpen = true;

        /** 0800 **/
        if (time == 8f) 
        {
            factoryDoor.OpenDoor();
            factoryDoor.StayOpen = true;
            StartCoroutine(LaunchConvoy(new Vector3(-38.4f ,0f ,-36.2f), new Vector3(-42.4f, 0f, 12.5f) , new Vector3(1f, 0f, 0f), enemies));
        }

        if (time == 10f)
        {
            factoryDoor.StayOpen = false;
            factoryDoor.CloseDoor();
        }

        /** 1830 **/
        if (time == 18.5f) 
        {
            cellDoor.StayOpen = false; 
            cellDoor.StayClose = true;
            factoryDoor.OpenDoor();
            factoryDoor.StayOpen = true;
            StartCoroutine(LaunchConvoy(new Vector3(-42.4f, 0f, 12.5f), new Vector3(-38.4f ,0f ,-36.2f) , new Vector3(0f, 0f, 1f), enemies));

        }
        /** 2100 **/
        if (time == 21f)
        {
            factoryDoor.StayOpen = false;
            factoryDoor.CloseDoor();
        }
    }

    public void StartTestCo()
    {
        StartCoroutine(Test());
    }

    // Test function for the Test btn
    // Demonstrate how to insert a paradigm sequence based on a click event
    // doesn't need to reside in RoutineManager, and can be inside and item script as well.
    IEnumerator Test()
    {
        ParadigmSO[] eventParadigms = new ParadigmSO[2];
        eventParadigms[0] = Resources.Load<ParadigmSO>("Routine/Paradigms/Paradigm Test 1");
        eventParadigms[1] = Resources.Load<ParadigmSO>("Routine/Paradigms/Paradigm Test 2");

        EnemyManager enemy = GameObject.Find("Leaving Room Guard 1").GetComponent<EnemyManager>();
        enemy.LoadEventParadigms(eventParadigms);
        enemy.InvokeEventParadigm();
        yield return null;
    }

    public void WellcomeTheConvoy()
    {
        ParadigmSO[] eventParadigms = new ParadigmSO[1];
        eventParadigms[0] = Resources.Load<ParadigmSO>("Routine/Paradigms/Storage Warehouse/0830 Storage Facility Wellcome Announce");
        EnemyManager enemy = GameObject.Find("Storage Warehouse Guard 1").GetComponent<EnemyManager>();
        enemy.LoadEventParadigms(eventParadigms);
        enemy.InvokeEventParadigm();
    }
    IEnumerator LaunchConvoy(Vector3 startPos, Vector3 endPos, Vector3 tailStartDirection , EnemyManager[] enemies )
    {
        if (enemies.Length == 0) 
        {
            Debug.Log("Convoy aborted - has 0 enemies");
            yield break;
        }
        float offset = 2.5f;
        int i = 0;
        Ai player = GameManager.Instance.PlayerAI;
        EnemyManager leadGuard = enemies[0];
        EnemyManager backGuard = enemies[enemies.Length - 1];

        _controller.FreezeController();
        foreach (EnemyManager enemy in enemies)
        {
            enemy.PauseAgentRoutine();
        }
        yield return null;

        foreach (EnemyManager enemy in enemies)
        {
            enemy.Ai.MoveToPoint(startPos + tailStartDirection.normalized * offset * i);
            i++;
        }
        backGuard.Ai.MoveToPoint(startPos + tailStartDirection.normalized * offset * i); // back guard makeing space for player
        player.MoveToPoint(startPos + tailStartDirection.normalized * offset * (i - 1)); // player move to the end - 1 place in the convoy 
        yield return null;

        // wait until lead guard goes into navigation mode
        yield return new WaitUntil(leadGuard.Ai.IsNavigating);

        // wait for all enemies and player to be in convoy formation
        bool convoyInFormation = false;
        while (!convoyInFormation)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            convoyInFormation = true & !player.IsNavigating();
            foreach (EnemyManager enemy in enemies)
            {
                convoyInFormation &= !enemy.Ai.IsNavigating();
                if (!convoyInFormation) break;
            }
        }

        // convoy launching
        foreach (EnemyManager enemy in enemies)
        {
            enemy.Ai.MoveToPoint(endPos);
        }
        player.MoveToPoint(endPos);
        yield return null;

        // wait until back guard goes into navigation mode   
        yield return new WaitUntil(backGuard.Ai.IsNavigating);
        // wait for convoy to arrive its destination
        while (leadGuard.Ai.IsNavigating())
        {
            yield return new WaitForSeconds(Time.deltaTime);
        }
 
        // convoy stoping
        foreach (EnemyManager enemy in enemies)
        {
            enemy.Ai.StopAgent();
        }
        player.StopAgent();

        // Activate the regular routine at each agent
        foreach (EnemyManager enemy in enemies)
        {
            enemy.ResumeCurrentParadigm();
        }
        _controller.UnFreezeController();
    }

    EnemyManager[] GetAgentsByName(string[] names)
    {
        List<EnemyManager> ret = new List<EnemyManager>();
        foreach (string name in names)
        {
            if (_enemies.ContainsKey(name)) ret.Add(_enemies[name]);
            else Debug.Log("No enemy with the name: " + name);
        }
        return ret.ToArray();
    }

    DoorController[] GetDoorsByName(string[] names)
    {
        List<DoorController> ret = new List<DoorController>();
        foreach (string name in names)
        {
            if (_doors.ContainsKey(name)) ret.Add(_doors[name]);
            else Debug.Log("No door with the name: " + name);
        }
        return ret.ToArray();
    }
}

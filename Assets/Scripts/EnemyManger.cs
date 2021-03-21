using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{

    private Paradigm[] paradigms;
    private int curr;
    private FieldOfView field;

    // Start is called before the first frame update
    void Start()
    {
        paradigms = GetComponent<InitRoutine>().Init();
        field = GetComponent<FieldOfView>();
        GameManager.Instance.Clock.TickEvent += updateParadigm;

        int time = GameManager.Instance.Clock.GetHour();
        for (int i = 0; i < paradigms.Length; i++)
        {
            curr = i;
            if (paradigms[curr].startTime <= time && time <= paradigms[curr].endTime)
            {
                break;
            }
        }
        GetComponent<Patrol>().ChangeRoute(paradigms[curr].patrolPath);
        GetComponent<Patrol>().StartPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (field.inField(GameObject.FindGameObjectWithTag("Player")))
        {
            checkRegulation();
        }
    }

    void checkRegulation()
    {
        GameObject[] inventory = GameManager.Instance.Inventory.inventoryItems;
        foreach (Regulation reg in paradigms[curr].regulations)
        {
            if (reg.isValid(inventory))
            {
                Debug.Log(reg.GetSeverity());
                break;
            }
        }
    }

    void updateParadigm()
    {
        int time = GameManager.Instance.Clock.GetHour();
        if (paradigms[curr].endTime <= time || time < paradigms[curr].startTime)
        {
            curr = (curr + 1) % paradigms.Length;
            GetComponent<Patrol>().ChangeRoute(paradigms[curr].patrolPath);
            GetComponent<Patrol>().StartPatrol();
        }
    }
}

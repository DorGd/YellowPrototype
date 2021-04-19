using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{

    private Paradigm[] paradigms;
    private int curr;
    private FieldOfView field;
    private Vector3 initPosition;
    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        paradigms = GetComponent<InitRoutine>().Init();
        field = GetComponent<FieldOfView>();
        GameManager.Instance.Clock.TickEvent += updateParadigm;
        GameManager.Instance.Clock.ResetEvent += ResetParadigm;

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

    private void ResetParadigm()
    {
        transform.position = initPosition;
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
        GetComponent<Patrol>().GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (field.InField())
        {
            checkRegulation();
        }
    }

    void checkRegulation()
    {
        GameManager instance = GameManager.Instance;
        Inventory inventory = instance.Inventory;
        GameObject[] inventoryItems = inventory.inventoryItems;
        paradigms[curr].action.Invoke();
        foreach (Regulation reg in paradigms[curr].regulations)
        {
            if (!reg.isValid(inventoryItems))
            {
                switch(reg.GetSeverity())
                {
                    case Regulation.Severity.Medium:
                        foreach (GameObject eq in inventoryItems)
                        {
                            if (eq == null)
                            {
                                continue;
                            }
                            foreach (GameObject feq in reg.forbiddenEquipment)
                            {
                                if (feq == null)
                                    continue;
                                if (feq.Equals(eq) || eq.gameObject.name.StartsWith(feq.gameObject.name))
                                {
                                    GameManager.Instance.Confiscate(eq);
                                }
                            }
                        }
                        break;
                    case Regulation.Severity.High:
                        Debug.Log("High");
                        foreach (GameObject eq in inventoryItems)
                        {
                            if (eq == null)
                                continue;
                            GameManager.Instance.Confiscate(eq);
                        }
                        GameObject.FindGameObjectWithTag("Player").transform.Find("helmet").gameObject.SetActive(false);
                        GameManager.Instance.ResetDay();
                        break;
                    case Regulation.Severity.Low:
                        if (reg.spawnPosition != null)
                        {
                            foreach (GameObject eq in reg.forbiddenEquipment)
                            {
                                if (eq == null)
                                    continue;
                                if (eq.name.StartsWith("chest") && transform.parent.gameObject.name.Equals("DocksGuard"))
                                {
                                    foreach (GameObject item in GameManager.Instance.Inventory.inventoryItems)
                                    {
                                        if (item == null)
                                            continue;
                                        if (item.name.StartsWith("chest") && !item.GetComponent<Chest>().open)
                                        {
                                            item.GetComponent<Chest>().Open();
                                            GameManager.Instance.Confiscate(item);
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                            GameObject.FindGameObjectWithTag("Player").transform.position = reg.spawnPosition.position;
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAI>().MoveToPoint(reg.spawnPosition.position);
                            //foreach (GameObject eq in reg.requiredEquipment)
                            //{
                            //    if (eq.name.Equals("Helmet"))
                            //    {
                            //        GameObject.Find("LivingQuartersSecurityDoorContainer").GetComponent<SecurityDoor>().ForceOpen(reg.spawnPosition.position);
                            //        GameObject.Find("LivingQuartersDoorContainer").GetComponent<SecurityDoor>().ForceOpen(reg.spawnPosition.position);
                            //    }
                            //}
                        }
                        break;
                }
            }
        }
    }

    void updateParadigm()
    {
        int time = GameManager.Instance.Clock.GetHour();
        if (((paradigms[curr].endTime > paradigms[curr].startTime) && paradigms[curr].endTime <= time) || // paradigm shift is on same day
            ((paradigms[curr].endTime < paradigms[curr].startTime) && (time >= paradigms[(curr + 1) % paradigms.Length].startTime && time < paradigms[(curr + 1) % paradigms.Length].endTime))) // paradigm shift is on next day
        {
            curr = (curr + 1) % paradigms.Length;
            GetComponent<Patrol>().ChangeRoute(paradigms[curr].patrolPath);
            GetComponent<Patrol>().StartPatrol();
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : Interactable
{
    public Transform[] boxPositions;

    public ParadigmSO closeBoxWarningParadigm;
    public ParadigmSO greatJobParadigm;
    private Chest[] boxes;
    private void Awake()
    {
        boxes = new Chest[boxPositions.Length];
    }
    public override Action[] CalcInteractions()
    {
        if (GameManager.Instance.inventory.IsInInventory(ItemType.Chest) && !(GameManager.Instance.inventory.GetHandItem() as Chest).open)
        {
            return new Action[] { Place };
        }
        if (GameManager.Instance.Clock.GetHour() >= closeBoxWarningParadigm.startTime && GameManager.Instance.Clock.GetHour() < closeBoxWarningParadigm.endTime)
        {
            EnemyManager docksGuard = GameObject.Find("Docks Guard 1").GetComponent<EnemyManager>();
            docksGuard.LoadEventParadigms(new ParadigmSO[] { closeBoxWarningParadigm });
            docksGuard.InvokeEventParadigm();
        }
        return new Action[] { };
    }

    private void Place()
    {
        GetComponent<Animator>().SetTrigger("Start");
        AudioManager.Instance.PlayOneShotCalcDist(AudioManager.SFX_conveyor, transform.position);
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i] == null)
            {
                boxes[i] = GameManager.Instance.inventory.GetHandItem() as Chest;
                boxes[i].transform.position = boxPositions[i].position;
                boxes[i].gameObject.SetActive(true);
                GameManager.Instance.inventory.DeleteItem(ItemType.Chest);
                GoodJob();
                return;
            }
        }
        Destroy(boxes[0].gameObject);
        for (int i = 1; i <boxPositions.Length; i++)
        {
            boxes[i - 1] = boxes[i];
            boxes[i] = null;
            boxes[i - 1].transform.position = boxPositions[i - 1].position;
        }
        Place();
    }

    private void GoodJob()
    {
        if (GameManager.Instance.Clock.GetHour() >= greatJobParadigm.startTime && GameManager.Instance.Clock.GetHour() < greatJobParadigm.endTime)
        {
            EnemyManager docksGuard = GameObject.Find("Docks Guard 1").GetComponent<EnemyManager>();
            docksGuard.LoadEventParadigms(new ParadigmSO[] { greatJobParadigm });
            docksGuard.InvokeEventParadigm();
        }
    }
}



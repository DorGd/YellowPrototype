using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecurityDoor : MonoBehaviour
{
    public GameObject Key;
    public int[] openTimes;
    public int[] closeTimes;
    public bool scheduleOpen;
    public bool forceOpen;
    //public Text getHelmetText;

    private int index = 0;
    private Coroutine countdown;
    void Start()
    {
        GameManager.Instance.Clock.TickEvent += OpenOnMorningAndEvening;
        GameManager.Instance.Clock.ResetEvent += ResetDoor;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player") || scheduleOpen || forceOpen)
            return;
        if (GameManager.Instance.Inventory.IsInInventory(Key, false))
        {
            GetComponent<Animator>().SetBool("Open", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Open", false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Equals("Player") || scheduleOpen || forceOpen)
            return;
        GetComponent<Animator>().SetBool("Open", false);
    }

    private void OpenOnMorningAndEvening()
    {
        if (forceOpen)
            return;
        int time = GameManager.Instance.Clock.GetHour();
        if (((openTimes[index] < closeTimes[index]) &&
            (time >= openTimes[index] && time < closeTimes[index])) ||
            ((openTimes[index] > closeTimes[index]) &&
            ((time >= openTimes[index] && time < 24) || (time < closeTimes[index]))))
        {
            scheduleOpen = true;
            GetComponent<Animator>().SetBool("Open", true);
        }
        else if (((openTimes[index] < closeTimes[index]) &&
            (time >= closeTimes[index])) ||
            ((openTimes[index] > closeTimes[index]) &&
            (time >= closeTimes[index] && time < openTimes[index])))
        {
            index = (index + 1) % openTimes.Length;
            scheduleOpen = false;
            GetComponent<Animator>().SetBool("Open", false);
        }
    }

    private void ResetDoor()
    {
        index = 0;
        scheduleOpen = false;
        GetComponent<Animator>().SetBool("Open", false);
    }
    //public void ForceOpen(Vector3 spawn)
    //{
    //    if (getHelmetText != null)
    //    {
    //        getHelmetText.gameObject.SetActive(true);
    //    }
    //    if (GetComponent<Animator>().GetBool("Open"))
    //        return;
    //    forceOpen = true;
    //    countdown = StartCoroutine(OneMinuteCountdown(spawn));
    //    GetComponent<Animator>().SetBool("Open", true);
    //}

    //private IEnumerator OneMinuteCountdown(Vector3 spawn)
    //{
    //    int timer = 60;
    //    while (timer > 0)
    //    {
    //        yield return new WaitForSeconds(1f);
    //        timer--;
    //        if (getHelmetText != null)
    //        {
    //            getHelmetText.text = "Get Helmet - Time: " + timer + " sec";
    //        }
    //    }
    //    GameObject.FindGameObjectWithTag("Player").transform.position = spawn;
    //    if (getHelmetText != null)
    //    {
    //        getHelmetText.gameObject.SetActive(false);
    //    }
    //    forceOpen = false;
    //    countdown = null;
    //}

    //public void StopCountdown()
    //{
    //    if (countdown == null)
    //        return;
    //    StopCoroutine(countdown);
    //    if (getHelmetText != null)
    //    {
    //        getHelmetText.gameObject.SetActive(false);
    //    }
    //    Debug.Log("Stop Countdown");
    //    forceOpen = false;
    //}
}



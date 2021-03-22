using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Clock : MonoBehaviour
{
    public event Action TickEvent;
    public event Action ResetEvent;
    private const float REAL_SECONDS_PER_INGAME_DAY = 60f * 13f;
    private bool firstTickInFrame = true;
    private bool firstTickEver = true;


    //private Transform clockHourHandTransform;
    //private Transform clockMinuteHandTransform;
    private float day = (1.0f/24.0f) * 6.0f;
    [SerializeField] private Text timeText;

    private void Awake()
    {
        //clockHourHandTransform = transform.Find("hourHand");
        //clockMinuteHandTransform = transform.Find("minuteHand");
        //timeText = transform.Find("timeText").GetComponent<Text>();
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
        float dayNormalized = day % 1f;

        //float rotationDegPerDay = 360f;
        //clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegPerDay);

        float hoursPerDay = 24f;
        //clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegPerDay * hoursPerDay);

        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

        float minutesPerHour = 60f;
        string minutsString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        timeText.text = hoursString + ":" + minutsString;
        StartCoroutine(Tick());
        //CheckForTick();
    }

    private IEnumerator Tick()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            TickEvent?.Invoke();
        }
    }

    private void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
        float dayNormalized = day % 1f;

        //float rotationDegPerDay = 360f;
        //clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegPerDay);

        float hoursPerDay = 24f; 
        //clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegPerDay * hoursPerDay);

        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

        float minutesPerHour = 60f;
        string minutsString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        timeText.text = hoursString + ":" + minutsString;
        //CheckForTick();

    }

    //void CheckForTick()
    //{
    //    if (timeText.text.EndsWith("0"))
    //    {
    //        if (firstTickInFrame)
    //        {
    //            if (!firstTickEver)
    //            {
    //                Debug.Log("Tik");
    //                TickEvent?.Invoke();
    //            }
    //            else
    //            {
    //                TickEvent?.Invoke();
    //                firstTickEver = false;
    //            }
    //            firstTickInFrame = false;
    //        }
    //    }
    //    else if (!firstTickInFrame)
    //    {
    //        firstTickInFrame = true;
    //    }
        
    //}

    public int GetHour()
    {
        return Convert.ToInt32(timeText.text.Substring(0, 2));
    }


    public void ResetDay()
    {
        StopAllCoroutines();
        day = (1.0f / 24.0f) * 6.0f;
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
        float dayNormalized = day % 1f;

        //float rotationDegPerDay = 360f;
        //clockHourHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegPerDay);

        float hoursPerDay = 24f;
        //clockMinuteHandTransform.eulerAngles = new Vector3(0, 0, -dayNormalized * rotationDegPerDay * hoursPerDay);

        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

        float minutesPerHour = 60f;
        string minutsString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        timeText.text = hoursString + ":" + minutsString;
        //CheckForTick();
        StartCoroutine(Tick());
        ResetEvent?.Invoke();
    }
}

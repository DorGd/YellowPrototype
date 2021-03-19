using UnityEngine;
using UnityEngine.UI;
using System;
public class Clock : MonoBehaviour
{
    public event Action TickEvent;
    private const float REAL_SECONDS_PER_INGAME_DAY = 60f * 5f;
    private bool firstTickInFrame = true;
    private bool firstTickEver = true;


    //private Transform clockHourHandTransform;
    //private Transform clockMinuteHandTransform;
    private float day;
    [SerializeField] private Text timeText;

    private void Awake()
    {
        //clockHourHandTransform = transform.Find("hourHand");
        //clockMinuteHandTransform = transform.Find("minuteHand");
        //timeText = transform.Find("timeText").GetComponent<Text>();
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
        CheckForTick();

    }

    void CheckForTick()
    {
        if (timeText.text.EndsWith("00"))
        {
            if (firstTickInFrame)
            {
                if (!firstTickEver)
                {
                    Debug.Log("Tik");
                    TickEvent?.Invoke();
                }
                else
                {
                    firstTickEver = false;
                }
                firstTickInFrame = false;
            }
        }
        else if (!firstTickInFrame)
        {
            firstTickInFrame = true;
        }
        
    }
}

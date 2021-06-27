using UnityEngine;
using UnityEngine.UI;
using System;
public class Clock : MonoBehaviour
{
    public event Action TickEvent;
    public float REAL_SECONDS_PER_INGAME_DAY = 60f * 10f; // public for debug, should be const private
    private bool firstTickInFrame = true;
    private bool firstTickEver = true;

    private float day;
    [SerializeField] private Text timeText;
    [SerializeField] private float initTime;
    private void Awake()
    {
        day = initTime / 24;
        Update();
    }

    private void Update()
    {
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
        float dayNormalized = day % 1f;

        float hoursPerDay = 24f; 

        string hoursString = Mathf.Floor(dayNormalized * hoursPerDay).ToString("00");

        float minutesPerHour = 60f;
        string minutsString = Mathf.Floor(((dayNormalized * hoursPerDay) % 1f) * minutesPerHour).ToString("00");

        timeText.text = hoursString + ":" + minutsString;
        CheckForTick();

    }

    void CheckForTick()
    {
        if (timeText.text.EndsWith("0"))
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

    public int GetHour()
    {
        return Convert.ToInt32(timeText.text.Substring(0, 2));
    }

    /// summary  
    /// return minuts as partial hour, means 30 m -> 0.5 h
    /// summary
    public float GetMinutes()
    {
        return Convert.ToInt32(timeText.text.Substring(3,2)) / 60f;
    }
}

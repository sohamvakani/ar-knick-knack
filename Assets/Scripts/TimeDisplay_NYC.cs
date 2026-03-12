using UnityEngine;
using TMPro;
using System;

public class TimeDisplay_NYC : MonoBehaviour
{
    public TextMeshPro timeText;
    private TimeZoneInfo nycTimeZone;

    void Start()
    {
        try
        {
            nycTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
        }
        catch
        {
            nycTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
        }
    }

    void Update()
    {
        DateTime nycTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, nycTimeZone);
        timeText.text = "New York Time\n" + nycTime.ToString("hh:mm:ss tt");
    }
}
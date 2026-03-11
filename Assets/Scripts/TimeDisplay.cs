using UnityEngine;
using TMPro;
using System;

public class TimeDisplay : MonoBehaviour
{
    public TextMeshPro timeText;
    private TimeZoneInfo chicagoTimeZone;

    void Start()
    {
        try
        {
            chicagoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");
        }
        catch
        {
            chicagoTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        }
    }

    void Update()
    {
        DateTime chicagoTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, chicagoTimeZone);
        timeText.text = "Chicago Time\n" + chicagoTime.ToString("hh:mm:ss tt");
    }
}
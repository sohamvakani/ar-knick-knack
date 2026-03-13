using UnityEngine;
using TMPro;
using System;

public class DayNightIcon : MonoBehaviour
{
    public TextMeshPro iconText;
    public string timeZoneId = "America/Chicago";
    private TimeZoneInfo localTimeZone;

    void Start()
    {
        try
        {
            localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        catch
        {
            localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
        }
    }

    void Update()
    {
        DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, localTimeZone);
        int hour = localTime.Hour;

        if (hour >= 6 && hour < 8)
{
    iconText.text = "Sunrise";
    iconText.color = new Color(1f, 0.6f, 0.3f); // orange
}
else if (hour >= 8 && hour < 17)
{
    iconText.text = "Daytime";
    iconText.color = new Color(1f, 0.9f, 0.2f); // yellow
}
else if (hour >= 17 && hour < 19)
{
    iconText.text = "Sunset";
    iconText.color = new Color(1f, 0.4f, 0.1f); // red orange
}
else
{
    iconText.text = "Nighttime";
    iconText.color = new Color(0.4f, 0.4f, 1f); // blue
}
    }
}
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class WeatherColorDisplay : MonoBehaviour
{
    public TextMeshPro conditionText;
    private string apiKey;
    private string city = "Chicago";
    private float updateInterval = 1200f;

    void Awake()
    {
        TextAsset configFile = Resources.Load<TextAsset>("config");
        if (configFile != null)
        {
            apiKey = configFile.text.Trim();
        }
        else
        {
            Debug.LogError("Config file not found!");
        }
    }

    void Start()
    {
        StartCoroutine(GetWeatherCondition());
        InvokeRepeating("RestartCoroutine", updateInterval, updateInterval);
    }

    void RestartCoroutine()
    {
        StartCoroutine(GetWeatherCondition());
    }

    IEnumerator GetWeatherCondition()
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=imperial";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            Debug.Log($"Color Weather Response: {request.downloadHandler.text}");

            if (request.result == UnityWebRequest.Result.Success)
            {
                WeatherColorResponse weather = JsonUtility.FromJson<WeatherColorResponse>(request.downloadHandler.text);
                string condition = weather.weather[0].main.ToLower();
                Debug.Log($"Color condition: {condition}");
                UpdateColor(condition);
            }
            else
            {
                Debug.LogError($"Color weather request failed: {request.error}");
                conditionText.text = "N/A";
                conditionText.color = Color.white;
            }
        }
    }

    void UpdateColor(string condition)
    {
        if (condition.Contains("rain") || condition.Contains("drizzle") || condition.Contains("thunderstorm"))
        {
            conditionText.text = "Rainy";
            conditionText.color = new Color(0.3f, 0.6f, 1f);
        }
        else if (condition.Contains("snow"))
        {
            conditionText.text = "Snowy";
            conditionText.color = Color.white;
        }
        else if (condition.Contains("clear"))
        {
            conditionText.text = "Sunny";
            conditionText.color = new Color(1f, 0.9f, 0.2f);
        }
        else if (condition.Contains("cloud"))
        {
            conditionText.text = "Cloudy";
            conditionText.color = new Color(0.7f, 0.7f, 0.7f);
        }
        else
        {
            conditionText.text = condition;
            conditionText.color = Color.white;
        }
    }
}

[System.Serializable]
public class WeatherColorResponse
{
    public WeatherColorMain main;
    public WeatherColorCondition[] weather;
}

[System.Serializable]
public class WeatherColorMain
{
    public float temp;
}

[System.Serializable]
public class WeatherColorCondition
{
    public string description;
    public string main;
}
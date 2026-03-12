using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class WeatherDisplay : MonoBehaviour
{
    public TextMeshPro weatherText;
    private string apiKey;
    private string city = "Chicago";
    private float updateInterval = 600f;

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
        StartCoroutine(GetWeather());
        InvokeRepeating("RestartWeatherCoroutine", updateInterval, updateInterval);
    }

    void RestartWeatherCoroutine()
    {
        StartCoroutine(GetWeather());
    }

    IEnumerator GetWeather()
    {
        string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=imperial";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            Debug.Log($"Weather API Response Code: {request.responseCode}");
            Debug.Log($"Weather API Response: {request.downloadHandler.text}");

            if (request.result == UnityWebRequest.Result.Success)
            {
                WeatherResponse weather = JsonUtility.FromJson<WeatherResponse>(request.downloadHandler.text);
                float temp = weather.main.temp;
                string description = weather.weather[0].description;
                string mainCondition = weather.weather[0].main.ToLower();

                weatherText.text = $"Chicago Weather\n{temp}°F\n{description}";
                UpdateTextColor(mainCondition);
            }
            else
            {
                Debug.LogError($"Weather request failed: {request.error}");
                weatherText.text = "Weather\nUnavailable";
                weatherText.color = Color.white;
            }
        }
    }

    void UpdateTextColor(string condition)
    {
        if (condition.Contains("rain") || condition.Contains("drizzle") || condition.Contains("thunderstorm"))
        {
            // Blue for rain
            weatherText.color = new Color(0.3f, 0.6f, 1f);
        }
        else if (condition.Contains("snow"))
        {
            // White for snow
            weatherText.color = Color.white;
        }
        else if (condition.Contains("clear"))
        {
            // Yellow for sunny
            weatherText.color = new Color(1f, 0.9f, 0.2f);
        }
        else if (condition.Contains("cloud"))
        {
            // Grey for cloudy
            weatherText.color = new Color(0.7f, 0.7f, 0.7f);
        }
        else
        {
            weatherText.color = Color.white;
        }
    }
}

[System.Serializable]
public class WeatherResponse
{
    public Main main;
    public Weather[] weather;
}

[System.Serializable]
public class Main
{
    public float temp;
}

[System.Serializable]
public class Weather
{
    public string description;
    public string main;
}
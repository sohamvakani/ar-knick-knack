using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class WeatherDisplay : MonoBehaviour
{
    public TextMeshPro weatherText;
    private string apiKey = "3696ea76f38f3467c41db66bb0573e1c";
    private string city = "Chicago";
    private float updateInterval = 600f; // 10 minutes

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

            if (request.result == UnityWebRequest.Result.Success)
            {
                WeatherResponse weather = JsonUtility.FromJson<WeatherResponse>(request.downloadHandler.text);
                weatherText.text = $"Chicago Weather\n{weather.main.temp}°F\n{weather.weather[0].description}";
            }
            else
            {
                weatherText.text = "Weather\nUnavailable";
            }
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
}
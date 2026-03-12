using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class WeatherDisplay_NYC : MonoBehaviour
{
    public TextMeshPro weatherText;
    private string apiKey = "3696ea76f38f3467c41db66bb0573e1c";
    private string city = "New York";
    private float updateInterval = 600f;

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
                WeatherResponse_NYC weather = JsonUtility.FromJson<WeatherResponse_NYC>(request.downloadHandler.text);
                weatherText.text = $"NYC Weather\n{weather.main.temp}°F\n{weather.weather[0].description}";
            }
            else
            {
                weatherText.text = "Weather\nUnavailable";
            }
        }
    }
}

[System.Serializable]
public class WeatherResponse_NYC
{
    public Main_NYC main;
    public Weather_NYC[] weather;
}

[System.Serializable]
public class Main_NYC
{
    public float temp;
}

[System.Serializable]
public class Weather_NYC
{
    public string description;
}
using UnityEngine;
using TMPro;

public class LocationDisplay_NYC : MonoBehaviour
{
    public TextMeshPro locationText;

    void Start()
    {
        locationText.text = "New York City\nNew York, NY";
    }
}
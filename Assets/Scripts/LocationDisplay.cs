using UnityEngine;
using TMPro;

public class LocationDisplay : MonoBehaviour
{
    public TextMeshPro locationText;

    void Start()
    {
        locationText.text = "Wrigley Field\nChicago, IL";
    }
}
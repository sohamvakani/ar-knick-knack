using UnityEngine;
using TMPro;

public class CustomDisplay : MonoBehaviour
{
    public TextMeshPro customText;

    void Start()
    {
        customText.text = "Wrigley Field\nOpened: 1914\nHome of the\nChicago Cubs";
    }
}
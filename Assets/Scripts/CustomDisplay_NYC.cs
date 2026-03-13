using UnityEngine;
using TMPro;

public class CustomDisplay_NYC : MonoBehaviour
{
    public TextMeshPro customText;

    void Start()
    {
        customText.text = "New York City\nPop: 8.3 Million\nHome of the\nEmpire State Building";
    }
}
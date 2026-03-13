using UnityEngine;
using TMPro;

public class ProximityDetector : MonoBehaviour
{
    public Transform otherCube; // drag NYC Multi Target here
    public GameObject proximityTextObject; // drag the floating text object here
    public float triggerDistance = 2.0f; // adjust this based on testing

    private bool isClose = false;

    void Update()
{
    if (otherCube == null || proximityTextObject == null)
    {
        Debug.LogError("Missing references!");
        return;
    }

    float distance = Vector3.Distance(transform.position, otherCube.position);
    Debug.Log($"Distance: {distance} | TriggerDistance: {triggerDistance} | IsClose: {isClose}");

    if (distance < triggerDistance)
    {
        if (!isClose)
        {
            isClose = true;
            proximityTextObject.SetActive(true);
            Debug.Log("TRIGGERED - showing text!");
        }

        // Keep updating position while close
        Vector3 midpoint = (transform.position + otherCube.position) / 2f;
        proximityTextObject.transform.position = midpoint + Vector3.up * 0.15f;
        proximityTextObject.transform.LookAt(Camera.main.transform);
        proximityTextObject.transform.Rotate(0, 180f, 0);
        proximityTextObject.transform.Rotate(0, 0, -90f); // fix sideways tilt
    }
    else
    {
        if (isClose)
        {
            isClose = false;
            proximityTextObject.SetActive(false);
            Debug.Log("RESET - hiding text");
        }
    }
}
}
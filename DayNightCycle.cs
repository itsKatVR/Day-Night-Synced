
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class DayNightCycle : UdonSharpBehaviour
{
    public TextMeshProUGUI sourceElement;
    public GameObject SkyLight;
    public float setTime = 60f; // Time for a full rotation in minutes

    private float targetRotation = 360f; // Full rotation angle in degrees
    [UdonSynced] public float currentRotation = 0f; // Current rotation progress
    private float rotationSpeed; // Speed calculated based on the time and rotation angle
    
    private void Start()
    {
        // Calculate rotation speed based on the desired time and rotation angle
        rotationSpeed = targetRotation / (setTime * 60f); // Convert setTime to seconds
    }

    private void Update()
    {
        // Update the rotation based on time
        currentRotation += rotationSpeed * Time.deltaTime;
        currentRotation %= targetRotation; // Keep the rotation value within 0 - targetRotation range

        // Apply rotation to the object
        transform.rotation = Quaternion.Euler(currentRotation, 0f, 0f);
        RequestSerialization();
        sourceElement.text = currentRotation.ToString();
    }

    public override void OnDeserialization() => currentRotation = currentRotation;
}
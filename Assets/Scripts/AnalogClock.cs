using System;
using UnityEngine;

public class AnalogClock : MonoBehaviour
{
    [Header("Clock Needles")]
    public Transform hourNeedle;
    public Transform minuteNeedle;
    public Transform secondNeedle;
    
    [Header("Clock Settings")]
    [Tooltip("Update frequency in seconds")]
    public float updateFrequency = 0.1f;
    
    private float timer;
    
    void Start()
    {
        // Initial time setup
        UpdateClockTime();
    }
    
    void Update()
    {
        // Update clock based on frequency
        timer += Time.deltaTime;
        if (timer >= updateFrequency)
        {
            UpdateClockTime();
            timer = 0f;
        }
    }
    
    void UpdateClockTime()
    {
        // Get current system time
        DateTime currentTime = DateTime.Now;
        
        // Extract time components
        int hours = currentTime.Hour % 12; // Convert to 12-hour format
        int minutes = currentTime.Minute;
        int seconds = currentTime.Second;
        int milliseconds = currentTime.Millisecond;
        
        // Calculate angles for each needle
        // Note: We're rotating on X-axis and accounting for your needle setup
        
        // Second needle: 360 degrees in 60 seconds
        // Adding smooth millisecond movement
        float secondAngle = (seconds + milliseconds / 1000f) * 6f; // 6 degrees per second
        
        // Minute needle: 360 degrees in 60 minutes
        // Adding smooth second movement
        float minuteAngle = (minutes + seconds / 60f) * 6f; // 6 degrees per minute
        
        // Hour needle: 360 degrees in 12 hours (720 minutes)
        // Adding smooth minute movement
        float hourAngle = (hours + minutes / 60f) * 30f; // 30 degrees per hour
        
        // Apply rotations on X-axis
        // Accounting for your prefab rotation (0, 180, 0) and needle rotation (90, 0, -90)
        // The initial needle rotation (90, 0, -90) means needles point in a specific direction
        // We need to add our calculated angles to this base rotation
        
        if (secondNeedle != null)
        {
            secondNeedle.localRotation = Quaternion.Euler(90f + secondAngle, 0f, -90f);
        }
        
        if (minuteNeedle != null)
        {
            minuteNeedle.localRotation = Quaternion.Euler(90f + minuteAngle, 0f, -90f);
        }
        
        if (hourNeedle != null)
        {
            hourNeedle.localRotation = Quaternion.Euler(90f + hourAngle, 0f, -90f);
        }
    }
    
    // Optional: Method to set a specific time (useful for testing)
    public void SetTime(int hours, int minutes, int seconds)
    {
        hours = hours % 12; // Convert to 12-hour format
        
        float secondAngle = seconds * 6f;
        float minuteAngle = (minutes + seconds / 60f) * 6f;
        float hourAngle = (hours + minutes / 60f) * 30f;
        
        if (secondNeedle != null)
        {
            secondNeedle.localRotation = Quaternion.Euler(90f + secondAngle, 0f, -90f);
        }
        
        if (minuteNeedle != null)
        {
            minuteNeedle.localRotation = Quaternion.Euler(90f + minuteAngle, 0f, -90f);
        }
        
        if (hourNeedle != null)
        {
            hourNeedle.localRotation = Quaternion.Euler(90f + hourAngle, 0f, -90f);
        }
    }
    
    // Optional: Method to get current time as string (for debugging)
    public string GetCurrentTimeString()
    {
        DateTime currentTime = DateTime.Now;
        return currentTime.ToString("HH:mm:ss");
    }
    
    void OnValidate()
    {
        // Clamp update frequency to reasonable values
        updateFrequency = Mathf.Clamp(updateFrequency, 0.01f, 1f);
    }
}
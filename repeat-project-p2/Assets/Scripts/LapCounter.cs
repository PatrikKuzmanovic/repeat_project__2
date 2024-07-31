using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{
    public GameObject car;
    public TMP_Text lapCounterText;
    public TMP_Text lapTimeText;
    public TMP_Text timerText;

    private int lapCount = 0;
    private float lapTime = 0f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        Debug.Log("LapCounter script started. Start Time: " + startTime);
        UpdateLapCountText();
        UpdateLapTimeText();
        UpdateTimerText();
    }
    void Update()
    {
        if (lapCount > 0)
        {
            lapTime = Time.time - startTime;
            UpdateLapTimeText();
            UpdateTimerText();
            Debug.Log("Current Lap Time: " + lapTime + " seconds");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called by: " + other.gameObject.name);
        if (other.gameObject == car)
        {
            lapCount++;
            Debug.Log("Lap completed: " + lapCount);

            lapTime = Time.time - startTime;
            Debug.Log("Lap Time: " + lapTime + " seconds");

            startTime = Time.time;
            Debug.Log("New Start Time: " + startTime);
        }
        else
        {
            Debug.Log("Trigger entered by a different object: " + other.gameObject.name);
        }
    }

    void UpdateLapCountText()
    {
        //lapCounterText.text = "Lap: " + lapCount;
        if (lapCounterText != null)
            lapCounterText.text = "Lap: " + lapCount;
    }

    void UpdateLapTimeText()
    {
        //lapTimeText.text = "Lap Time: " + lapTime.ToString("F2") + " seconds";
        if (lapTimeText != null)
            lapTimeText.text = "Lap Time: " + lapTime.ToString("F2");
    }

    void UpdateTimerText()
    {
        //float elapsedTime = Time.time - startTime;
        //timerText.text = "Time: " + elapsedTime.ToString("F2") + " seconds";
        if (timerText != null)
        {
            float elapsedTime = Time.time - startTime;
            timerText.text = "Time: " + elapsedTime.ToString("F2");
        }
    }
}
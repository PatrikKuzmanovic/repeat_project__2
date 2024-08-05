using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{
    public GameObject car;
    public TMP_Text lapCounterText;
    public TMP_Text lapTimeText;
    public TMP_Text timerText;
    public TMP_Text lapProgressText;

    private int lapCount = 0;
    private int totalLaps = 3;
    private float lapTime = 0f;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
        UpdateLapCountText();
        UpdateLapTimeText();
        UpdateTimerText();
        UpdateLapProgressText();
    }
    void Update()
    {
        if (lapCount > 0)
        {
            lapTime = Time.time - startTime;
            UpdateLapTimeText();
            UpdateTimerText();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == car)
        {
            lapCount++;

            lapTime = Time.time - startTime;

            startTime = Time.time;

            UpdateLapProgressText();
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
    void UpdateLapProgressText()
    {
        if (lapProgressText != null)
            lapProgressText.text = "Lap: " + lapCount + "/" + totalLaps;
    }
}
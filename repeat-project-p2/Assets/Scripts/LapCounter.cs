using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{
    public GameObject car;
    public GameObject aiCar;
    public TMP_Text lapCounterText;
    public TMP_Text lapTimeText;
    public TMP_Text timerText;
    public TMP_Text lapProgressText;
    public TMP_Text finishMessageText;
    public TMP_Text bestLapText;
    public TMP_Text positionText;

    private int lapCount = 0;
    private int totalLaps = 3;
    private float lapTime = 0f;
    private float startTime;
    private float bestLapTime = float.MaxValue;

    private bool raceFinished = false;

    void Start()
    {
        startTime = Time.time;
        UpdateLapCountText();
        UpdateLapTimeText();
        UpdateTimerText();
        UpdateLapProgressText();

        if (finishMessageText != null)
            finishMessageText.gameObject.SetActive(false);

        if (bestLapText != null)
            bestLapText.gameObject.SetActive(false);

        if (positionText != null)
            positionText.gameObject.SetActive(true);
    }
    void Update()
    {
        if (raceFinished)
            return;

        if (lapCount > 0)
        {
            lapTime = Time.time - startTime;
            UpdateLapTimeText();
            UpdateTimerText();
        }

        UpdatePositionText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == car)
        {
            if (raceFinished)
                return;
            lapTime = Time.time - startTime;

            if (lapTime < bestLapTime)
            {
                bestLapTime = lapTime;
            }

            lapCount++;

            startTime = Time.time;

            UpdateLapCountText();
            UpdateLapProgressText();

            if (lapCount >= totalLaps)
            {
                EndRace();
            }
        }
    }

    void EndRace()
    {
        raceFinished = true;

        if (finishMessageText != null)
        {
            finishMessageText.gameObject.SetActive(true);
            finishMessageText.text = "Race finished!";
        }

        if (bestLapText != null)
        {
            bestLapText.gameObject.SetActive(true);
            bestLapText.text = "Best Lap Time: " + FormatTime(bestLapTime);
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

    void UpdatePositionText()
    {
        if (positionText == null || car == null || aiCar == null)
            return;

        float playerDistance = Vector3.Distance(car.transform.position, transform.position);
        float aiCarDistance = Vector3.Distance(aiCar.transform.position, transform.position);

        if (playerDistance < aiCarDistance)
        {
            positionText.text = "Position: 2/2";
        }
        else
        {
            positionText.text = "Position: 1/2";
        }
    }

    string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        int milliseconds = (int)((time - (minutes * 60 + seconds)) * 1000);
        return $"{minutes:00}:{seconds:00},{milliseconds:000}";
    }
}
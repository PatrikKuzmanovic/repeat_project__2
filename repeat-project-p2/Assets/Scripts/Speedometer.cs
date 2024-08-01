using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public TMP_Text speedText;

    private void Update()
    {
        float speed = carRigidbody.velocity.magnitude;

        float speedKmh = speed * 3.6f;

        if (speedText != null)
        {
            speedText.text = "Speed: " + speedKmh.ToString("F1") + " km/h";
        }
    }
}

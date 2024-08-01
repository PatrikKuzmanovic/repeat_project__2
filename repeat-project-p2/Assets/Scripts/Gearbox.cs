using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gearbox : MonoBehaviour
{
    public Rigidbody carRigidbody;
    public TMP_Text gearText;

    private int currentGear = 1;
    private float speedKmh;

    void Start()
    {
        UpdateGearText();
    }

    void Update()
    {
        float speed = carRigidbody.velocity.magnitude;
        speedKmh = speed * 3.6f;

        bool isReversing = Vector3.Dot(carRigidbody.velocity, transform.forward) < 0;

        UpdateGear(isReversing);
    }

    void UpdateGear(bool isReversing)
    { 
        if (isReversing)
        {
            currentGear = 0;
        }
        else 
        {
            if (speedKmh < 20)
                currentGear = 1;
            else if (speedKmh < 45)
                currentGear = 2;
            else if (speedKmh < 70)
                currentGear = 3;
            else if (speedKmh < 95)
                currentGear = 4;
            else if (speedKmh < 120)
                currentGear = 5;
            else
                currentGear = 6;
        }
        

        UpdateGearText();
    }

    void UpdateGearText()
    {
        if (gearText != null)
        {
            if (currentGear == 0)
                gearText.text = "Gear: R";
            else
            gearText.text = "Gear: " + currentGear;
        }
    }
}

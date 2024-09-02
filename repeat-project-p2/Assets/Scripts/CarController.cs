using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBrake;
    private bool isBraking, isReversing;

    [SerializeField] private float accelerate, brakeFroce, maxSteerAngle, reverseForce;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    [SerializeField] private AudioClip engineSoundHighOff;
    private AudioSource audioSource;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        audioSource= rb.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (engineSoundHighOff != null)
        {
            audioSource.clip = engineSoundHighOff;
            audioSource.loop = true;
            audioSource.Play();
            audioSource.Pause();
        }
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleAcceleration();
        SteeringHandler();
        UpdateWheels();
        UpdateEngineSound();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        isBraking = Input.GetKey(KeyCode.S);
        isReversing = rb.velocity.magnitude < 0.1f && isBraking;
    }
    private void HandleAcceleration()
    {
        if (isReversing)
        {
            ApplyReverseForce();
        }
        else
        {
            ApplyForwardForce();
        }

        ApplyBraking();
    }

    private void ApplyForwardForce()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * accelerate;
        frontRightWheelCollider.motorTorque = verticalInput * accelerate;
    }

    private void ApplyReverseForce()
    {
        frontLeftWheelCollider.motorTorque = -1 * reverseForce;
        frontRightWheelCollider.motorTorque = -1 * reverseForce;
        rearLeftWheelCollider.motorTorque = -1 * reverseForce;
        rearRightWheelCollider.motorTorque = -1 * reverseForce;
    }
    private void ApplyBraking()
    {
        currentBrake = isBraking && !isReversing ? brakeFroce : 0f;
        frontRightWheelCollider.brakeTorque = currentBrake;
        frontLeftWheelCollider.brakeTorque = currentBrake;
        rearLeftWheelCollider.brakeTorque = currentBrake;
        rearRightWheelCollider.brakeTorque = currentBrake;
    }
    private void SteeringHandler()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

    private void UpdateEngineSound()
    {
        if (rb.velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            audioSource.UnPause();
        }
        else if (rb.velocity.magnitude <= 0.1f && audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        audioSource.pitch = 0.5f + (rb.velocity.magnitude / 10f);
    }
}
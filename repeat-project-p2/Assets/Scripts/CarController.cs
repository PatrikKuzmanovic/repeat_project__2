using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentBreak;
    private bool isBreaking;

    [SerializeField] private float accelerate, breakFroce, maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

    private void FixedUpdate()
    {
        GetInput();
        HandleAcceleration();
        SteeringHandler();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        verticalInput = Input.GetAxis("Vertical");

        isBreaking = Input.GetKey(KeyCode.S);
    }
    private void HandleAcceleration()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * accelerate;
        frontRightWheelCollider.motorTorque = verticalInput * accelerate;
        currentBreak = isBreaking ? breakFroce : 0f;
        ApplyBreaking();
    }
    private void ApplyBreaking()
    {
        frontRightWheelCollider.brakeTorque = currentBreak;
        frontLeftWheelCollider.brakeTorque = currentBreak;
        rearLeftWheelCollider.brakeTorque = currentBreak;
        rearRightWheelCollider.brakeTorque = currentBreak;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterBody : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpForce;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float secondsToMaxSpeed;
    [SerializeField] private float secondsToStandStill;
    [SerializeField] private float turnResistance;

    private Jump jump;

    private Displacement displacement;
    private float currentSpeed;
    private float maxSpeed;
    private Vector3 inputDirection;
    private Vector3 previousDirection = Vector3.zero;
    private Coroutine accelerationCoroutine;

    private void Awake()
    {
        jump = new Jump(rb, jumpForce);
        displacement = new Displacement(secondsToMaxSpeed, secondsToStandStill, turnResistance);
        maxSpeed = walkSpeed;
    }

    private void FixedUpdate()
    {
        currentSpeed = Mathf.SmoothStep(0, maxSpeed, displacement.GetSpeedPercentage());
        rb.velocity = inputDirection * currentSpeed;
    }

    public void Jump()
    {
        if (jump != null) { jump.PerformJump(); }        
    }

    public void Locomote(Vector3 inputDirection)
    {
        this.inputDirection = inputDirection;
        displacement.ApplyTurnDamping(previousDirection, inputDirection);
        previousDirection = inputDirection;

        if (accelerationCoroutine != null) { StopCoroutine(accelerationCoroutine); }
        accelerationCoroutine = StartCoroutine(displacement.Accelerate(true));
    }

    public void StopLocomotion()
    {
        if (accelerationCoroutine != null) { StopCoroutine(accelerationCoroutine); }
        accelerationCoroutine = StartCoroutine(displacement.Accelerate(false));
    }
}

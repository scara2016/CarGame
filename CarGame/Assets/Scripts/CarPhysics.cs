using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
    Rigidbody playerRigidbody;
    Collider playerCollider;
    PlayerMovement playerMovement;
    public float dragCoefficient= 0.5f;
    public float downforceCoefficient = 0.5f;
    private Vector3 totalForce;
    private Vector3 drag;
    private Vector3 downForce;
    private Vector3 playerVelocity;
    public Vector3 TotalForce
    {
        get
        {
            return totalForce;
        }
    }
    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void FixedUpdate()
    {
        playerVelocity = playerRigidbody.velocity;
        drag = dragCoefficient * 0.5f * playerVelocity.sqrMagnitude*(-playerVelocity.normalized);
        if (playerMovement.IsGrounded())
            downForce = downforceCoefficient * 0.5f * playerVelocity.sqrMagnitude * (Vector3.down);
        else
            downForce = Vector3.zero;
        calculateTotalForce();
    }

    private void calculateTotalForce()
    {
        totalForce = drag+downForce;
    }

}

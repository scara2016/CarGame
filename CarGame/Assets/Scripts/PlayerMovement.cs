using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private PlayerInput playerInput;
    PlayerControls playerControls;
    public float speed = 5f;
    private CarPhysics carPhysics;
    private Vector3 physicsForces;
    public float turnSpeed;
    public float recoilForce = 20f;
    private float distToGround;
    private Collider playerCollider;
    bool gameEnded = false;
    public bool GameEnded
    {
        set
        {
            gameEnded = value;
        }
    }


    private void Awake()
    {
        playerCollider = GetComponent<Collider>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerControls = new PlayerControls();
        carPhysics = GetComponent<CarPhysics>();
        playerControls.Player.Enable();
        playerControls.Player.Jump.performed += Jump;
        distToGround = playerCollider.bounds.extents.y / 2;
        playerRigidbody.centerOfMass = new Vector3(playerRigidbody.centerOfMass.x, 0.1f, playerRigidbody.centerOfMass.z);
    }

    private void FixedUpdate()
    {
        if (!gameEnded)
        {
            if (IsGrounded())
            {
                physicsForces = carPhysics.TotalForce;
                Vector3 inputVector = new Vector3();
                Vector3 inputRotate = new Vector3();
                inputRotate.y = playerControls.Player.Movement.ReadValue<Vector2>().x;
                inputVector.x = playerControls.Player.Movement.ReadValue<Vector2>().x * playerControls.Player.Movement.ReadValue<Vector2>().y;
                inputVector.z = playerControls.Player.Movement.ReadValue<Vector2>().y;
                if (playerRigidbody.velocity.magnitude < 3f)
                {
                    inputRotate.y = 0;
                    inputVector.x = 0;
                }
                playerRigidbody.AddRelativeForce(inputVector * speed, ForceMode.Force);
                playerRigidbody.AddForce(physicsForces, ForceMode.Force);
                if (playerRigidbody.velocity.magnitude < 3)
                {
                    inputRotate = Vector3.zero;
                }
                
                transform.Rotate(inputRotate * turnSpeed, Space.Self);
                
                Debug.Log(inputRotate * turnSpeed);
            }
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            playerRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag.Equals("Terrain"))
        {
            playerRigidbody.AddForce(Vector3.up * recoilForce, ForceMode.Impulse);
            playerRigidbody.AddForce(-playerRigidbody.velocity, ForceMode.Impulse);
            Debug.Log("Bonk");
        }
    }
}

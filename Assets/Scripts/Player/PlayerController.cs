using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [Header("Movement")]
    public float speed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    public MovementState state;
    public enum MovementState {
        dashing
    }

    public float dashSpeed;
    public bool dashing;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody body;
    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    

    // Update is called once per frame
    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);
        MyInput();
        SpeedControl();

        // handle drag
        if (grounded) {
            body.drag = groundDrag;
        } else {
            body.drag = 0;
        }
    }
    

    private void FixedUpdate() {
        MovePlayer();
    }

    private void StateHandler() {
        if (dashing) {
            state = MovementState.dashing;
            speed = dashSpeed;

        }
    }
    private void MovePlayer() {
        // Calculate movement direction
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        // on ground
        if (grounded) {
            body.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        }
        
        // in air
        else if (!grounded) {
            body.AddForce(moveDirection.normalized * speed * 10f * airMultiplier, ForceMode.Force);
        }
    }
    private void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Jump action
        if (Input.GetKey(jumpKey) && readyToJump && grounded) {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void SpeedControl() {
        Vector3 flatVel = new Vector3(body.velocity.x, 0f, body.velocity.z);

        // limit velocity if needed
        if (dashing) {
            if (flatVel.magnitude > speed + 20) {
                Vector3 limitedVel = flatVel.normalized * (speed + 20);
                body.velocity = new Vector3(limitedVel.x, body.velocity.y, limitedVel.z);
            }
        } else if (flatVel.magnitude > speed) {
            Vector3 limitedVel = flatVel.normalized * speed;
            body.velocity = new Vector3(limitedVel.x, body.velocity.y, limitedVel.z);

        }
    }

    private void Jump() {
        // reset y velocity
        body.velocity = new Vector3(body.velocity.x, 0f, body.velocity.z);
        body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        readyToJump = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Damage collider") {
            Debug.Log("Ouch");
        }
    }
}

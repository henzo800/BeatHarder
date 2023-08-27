using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController instance;
    public CharacterData characterData;
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

    public float health;
    public bool dashing;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask ground;
    bool grounded;

    float horizontalInput;
    float verticalInput;
    float timePassed = 0f;

    Vector3 moveDirection;
    Rigidbody body;
    public bool isControllable = true;
    
    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
        // Gets the starting time of the song
        timePassed = GameManager.instance.getAudioSource();
        health = characterData.HEALTH;
        speed = characterData.SPEED;
    }

    

    // Update is called once per frame
    private void Update()
    {
        timePassed += Time.deltaTime;
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
        if(isControllable) {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }else{
            horizontalInput = 0;
            verticalInput = 0;
        }

        // Jump action
        if (Input.GetKey(jumpKey) && readyToJump && grounded && isControllable) {
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

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Damage collider") {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(characterData.DAMAGE);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0) {
            // you died!
            PlayerUIController.instance.Death();
        }
    }



    public bool IsInTime() {
        float lastBeat = (GameManager.instance.getAudioSource() * 1000) % GameManager.instance.beatLength;
        float nextBeat = GameManager.instance.beatLength - lastBeat;

        if (lastBeat <= 100f || nextBeat <= 100f) {
            return true;
        }
        return false;
    }
}

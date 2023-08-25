using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody body;

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    

    // Update is called once per frame
    private void Update()
    {
        MyInput();
    }
    

    private void FixedUpdate() {
        MovePlayer();
    }
    private void MovePlayer() {
        // Calculate movement direction
        moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        body.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }
    void MyInput() {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}

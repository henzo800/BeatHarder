using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody body;
    private PlayerController playerMove;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerController>();
    }

    private void Dash() {
        Vector3 forceToApply = orientation.forward * dashForce + orientation.up * dashUpwardForce;

        body.AddForce(forceToApply, ForceMode.Impulse);

        //Invoke(nameof(ResetDash), dashDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dashKey)) {
            Dash();
        }
    }
}

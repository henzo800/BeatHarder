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
    public float dashCd;
    public float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.LeftShift;

    // Start is called before the first frame update
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerController>();
    }

    private void Dash() {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;

        playerMove.dashing = true;

        Transform forwardT = playerCam;
        Vector3 direction = GetDirection(forwardT);

        Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;
        delayedForceToApply = forceToApply;
        Invoke(nameof(DelayedDashForce), 0.025f);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce() {
        body.AddForce(delayedForceToApply, ForceMode.VelocityChange);
    }

    private void ResetDash() {
        playerMove.dashing = false;
    }

    private Vector3 GetDirection(Transform forwardT) {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        
        return direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dashKey)) {
            if (PlayerController.instance.IsInTime()) {
                Debug.Log("Dash");
                Dash();
            } else {
                Debug.Log("Failed Dash");
            }
        }

        // if (dashCdTimer > 0) {
        //     dashCdTimer -= Time.deltaTime;
        // }
    }
}

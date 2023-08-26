using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public Transform orientation;

    public GameObject player;
    float xRotation;
    float yRotation;
    public ParticleSystem muzzleFlash;
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.transform.rotation = Quaternion.Euler(0, yRotation, 0);

        transform.position = orientation.position;
    }
    void Shoot ()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            IDamageable target = hit.transform.GetComponent<IDamageable>();
            
            if (target != null) 
            {
                target.TakeDamage(PlayerController.instance.characterData.DAMAGE);

            }

        }
    }
}

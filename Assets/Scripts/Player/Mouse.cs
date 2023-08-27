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
    public ParticleSystem gunMuzzleFlash;
    public Animator swordAnimator;
    public Transform MeleePoint;
    public GameObject lightningBolt;

    public bool canAttack = false;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0)){
            if (IsInTime()) {
                if (PlayerController.instance.characterData.WEAPON == "Ranged") { // if ranged
                    Shoot();
                } else if (PlayerController.instance.characterData.WEAPON == "Melee") { // if melee
                    Melee();
                }
            } else {
                print("Not in time");
            }
        }
        
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
        gunMuzzleFlash.Play();

        lightningBolt.SetActive(true);
        StartCoroutine(DeactivateLightning());

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            Debug.Log("Shot Hit: " + hit.transform.name);

            IDamageable target;
            if (hit.transform.TryGetComponent<IDamageable>(out target)) 
            {
                target.TakeDamage(PlayerController.instance.characterData.DAMAGE);
            }

        }
    }

    IEnumerator DeactivateLightning() {
        yield return new WaitForSeconds(0.5f);
        lightningBolt.SetActive(false);
    }
    void Melee() {
        swordAnimator.SetTrigger("downwardSlash");
        
        Collider[] hitColliders = Physics.OverlapBox(MeleePoint.position, new Vector3(1,1,1));
        foreach(Collider collider in hitColliders){
            Debug.Log("Melee Hit: " + collider.transform.name);

            IDamageable target;
            if (collider.transform.TryGetComponent<IDamageable>(out target)) 
            {
                target.TakeDamage(PlayerController.instance.characterData.DAMAGE);
            }
        }
    }

    private bool IsInTime() {
        float lastBeat = GameManager.instance.getAudioSource() * 1000 % GameManager.instance.beatLength;
        float nextBeat = GameManager.instance.beatLength - lastBeat;

        return lastBeat <= 150f || nextBeat <= 150f;
    }

}

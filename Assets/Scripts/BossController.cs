using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // player object
    private GameObject player;
    Vector3 playerPosition;

    // beatmap object

    // attack objects
    public GameObject pinIndicator; // animation for pin
    public GameObject pinDamage; // damage object
    public GameObject sweepIndicator; // animation for sweep
    public GameObject sweepDamage; // damage object for sweep
    public GameObject projectileIndicator; // indicator for projectile
    public GameObject projectileDamage; // damage object for projectile
    public GameObject minionSpawner;

    // bpm
    public float bpm;
    public float indicatorLength; // length, in beats of indication before the damage frame

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            PinAttack();
        }
    }

    // Pin
    void PinAttack() {
        // start indicator animation
        // Instantiate(pinIndicator, PlayerControl.transformInstance);
        Instantiate(pinIndicator, playerPosition, Quaternion.Euler(0f, 0f, 0f));
        
        playerPosition = player.transform.position;
        playerPosition.y = 0f;

        // do damage
        Invoke("PinDamage", 2.5f);
    }

    void PinDamage() {
        Instantiate(pinDamage, playerPosition, Quaternion.Euler(0f, 0f, 0f));
    }

    // Sweep
    void SweepAttack() {

    }

    // 
    void SweepDamage() {

    }

    // projectile attack
    void ProjectileAttack() {

    }

    void ProjectileDamage() {

    }

    void SpawnMinion() {

    }

    
}

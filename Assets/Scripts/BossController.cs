using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // player object

    // beatmap object

    // attack objects
    public GameObject pinIndicator; // animation for pin
    public GameObject pinDamage; // damage object
    public GameObject sweepIndicator; // animation for sweep
    public GameObject sweepDamage; // damage object for sweep
    public GameObject projectile;
    public GameObject minionSpawner;

    // bpm
    public float bpm;
    public float indicatorLength; // length, in beats of indication before the damage frame

    // Start is called before the first frame update
    void Start()
    {
        
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
        Instantiate(pinIndicator, new Vector3(0f, 0.1f, 0f), Quaternion.Euler(0f, 0f, 0f));

        // do damage
        Invoke("PinDamage", indicatorLength * 60f / bpm);
    }

    void PinDamage() {
        
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

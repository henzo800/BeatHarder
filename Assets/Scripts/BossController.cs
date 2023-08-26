using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    // player object
    private GameObject player;
    Vector3 playerPosition;

    // beatmap object

    // attack objects
    public GameObject pinIndicator; // animation for pin
    public GameObject pinDamage; // damage object
    public GameObject sweepParticle; // particle for sweep
    public int numParticles = 24;
    public GameObject projectileIndicator; // indicator for projectile
    public GameObject projectileDamage; // damage object for projectile
    public GameObject minionSpawner;
    public float health;
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
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            PinAttack();
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SweepAttack();
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
        // Invoke("PinDamage", 2.5f);
        StartCoroutine(PinDamage(playerPosition));
    }

    IEnumerator PinDamage(Vector3 playerPosition) {
        yield return new WaitForSeconds(2.5f);
        Instantiate(pinDamage, playerPosition, Quaternion.Euler(0f, 0f, 0f));
    }

    // Sweep
    void SweepAttack() {
        Vector3 position = transform.position + new Vector3(0f, 0.5f, 0f);
        for (int i = 0; i < numParticles; i++) {
            Quaternion rotation = Quaternion.Euler(0f, 90f + 180f / numParticles * i, 0);
            GameObject particle = Instantiate(sweepParticle, position, rotation);
        }
    }

    // projectile attack
    void ProjectileAttack() {

    }

    void ProjectileDamage() {

    }

    void SpawnMinion() {

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}

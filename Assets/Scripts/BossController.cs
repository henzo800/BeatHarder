using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    // player object
    private GameObject player;
    Vector3 playerPosition;

    public CharacterData characterData;

    // beatmap object

    
    // bpm
    public float bpm;
    public float indicatorLength; // length, in beats of indication before the damage frame

    public float health;
    public static BossController instance;

    public float getHealth() {
        return health;
    }

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        health = characterData.HEALTH;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}

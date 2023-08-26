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

    public static BossController instance;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        characterData.HEALTH -= damage;
        if (characterData.HEALTH <= 0) {
            Destroy(gameObject);
        }
    }
}

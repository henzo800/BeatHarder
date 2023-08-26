using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinDamageController : MonoBehaviour
{
    public float damage = 1f;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Invoke("Destroy", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Destroy() {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}

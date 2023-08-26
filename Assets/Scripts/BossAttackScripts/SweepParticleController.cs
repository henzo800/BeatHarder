using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweepParticleController : MonoBehaviour
{
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = transform.forward * speed * Time.deltaTime;
        transform.position += velocity;
    }

    void Destroy() 
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        lifetime -= Time.deltaTime;
        if(lifetime < 0){
            //Dies of old age
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        //Damage hit object
        Destroy(this.gameObject);
    }
}

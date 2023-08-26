using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IDamageable
{
    public float speed = 5;
    public float lifetime = 10;
    public float damage = 10;
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
    void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.name);
        
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
        {
            target.TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        transform.rotation = PlayerController.instance.transform.rotation;
    }
}

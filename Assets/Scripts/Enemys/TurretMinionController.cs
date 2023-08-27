using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMinionController : MonoBehaviour, IDamageable
{
    public CharacterData characterData;
    public Transform mount;
    public GameObject bullet;
    public float health;
    public float ShotSpeedOverride;
    public bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        health = characterData.HEALTH;
    }

    // Update is called once per frame
    void Update()
    {
        mount.LookAt(Camera.main.transform.position);
        if(ShotSpeedOverride > 0 && canShoot){
            Fire(10,10);
            StartCoroutine(ShotDelay());
        }
        if(canShoot){
            Fire(10,100);
        }
    }

    IEnumerator ShotDelay(){
        canShoot = false;
        
        yield return new WaitForSeconds(ShotSpeedOverride);

        canShoot = true;
    }

    void Fire(float bulletSpeed, float bulletLifetime) {
        GameObject currentBullet = Instantiate(bullet, mount.transform.position + mount.transform.forward * 0.5f, mount.transform.rotation);
        currentBullet.GetComponent<BulletController>().speed = bulletSpeed;
        currentBullet.GetComponent<BulletController>().lifetime = bulletLifetime;

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0){
            Destroy(this.gameObject);
        }
    }
}

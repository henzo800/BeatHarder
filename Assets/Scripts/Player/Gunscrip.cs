
using UnityEngine;

public class Gunscrip : MonoBehaviour
{

    public float damage = 10f;
    public float range = 100f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;

    // Update is called once per frame
    void Update ()

    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }


    }


    void Shoot ()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);

            IDamageable target = hit.transform.GetComponent<IDamageable>();
            
            if (target != null) 
            {
                target.TakeDamage(damage);

            }

        }
    }

}
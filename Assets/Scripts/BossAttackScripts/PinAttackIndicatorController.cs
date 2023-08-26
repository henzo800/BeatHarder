using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinAttackIndicatorController : MonoBehaviour
{
    public float indicatorTime = 3f;
    public float age = 0f;
    public bool destroy = false;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        Vector3 position = player.transform.position;
        position.y = 0f;
        transform.position = position;
    }   

    

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 180f * Time.deltaTime, 0f);
        if (age >= indicatorTime) {
            destroy = true;
        }
        age += Time.deltaTime;
        if (destroy == true) {
            transform.position += new Vector3(0f, -10f, 0f);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}

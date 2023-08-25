using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 0.05f;

    public static Transform transformInstance;
    void Awake() {
        transformInstance = this.gameObject.transform;
    }
    
    // Update is called once per frame
    void Update() {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        
        Vector3 moveDirection = new Vector3(xMove, 0.0f, zMove);

        transform.position += moveDirection * speed;
    }
}

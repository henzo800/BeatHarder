using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinAttackIndicatorController : MonoBehaviour
{
    public float indicatorLength = 3f;
    public bool isAnimated = true;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Despawn", indicatorLength);
    }

    // Update is called once per frame
    void Update()
    {
        if(isAnimated){
            transform.Rotate(0f, 0.5f, 0f);
            transform.position += new Vector3(0f, 0.5f * Time.deltaTime, 0f);
        }

    }

    void Despawn() {
        isAnimated = false;
        //Destroy(this.gameObject);
    }
}

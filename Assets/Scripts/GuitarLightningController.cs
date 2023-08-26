using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarLightningController : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Destroy() {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}

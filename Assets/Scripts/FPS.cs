using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Transform targetPosition;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = targetPosition.position;
    }
}

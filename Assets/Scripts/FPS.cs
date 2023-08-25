using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
<<<<<<< HEAD
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
    public Transform targetPosition;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = targetPosition.position;
>>>>>>> 3d9086dd0ebcf5f77778eb43723d7e183d845a9f
    }
}

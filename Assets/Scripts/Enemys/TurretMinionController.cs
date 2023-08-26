using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMinionController : MonoBehaviour
{
    public Transform mount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mount.LookAt(Camera.main.transform.position);
    }
}

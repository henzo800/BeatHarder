using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bpmManager : MonoBehaviour
{
    // SpyAir Imagination test
    public float bpm;
    private float timing;
    private float pulse;
    float currentTime;

    private void Start() {
        timing = 60000.0f / bpm;
        pulse = timing / 2.0f;
        currentTime = GameManager.instance.getAudioSource();
        
    }
    // Update is called once per frame
    private void Update()
    {
        currentTime += Time.deltaTime;
        float isPulse = currentTime * 1000f % pulse;
        if (isPulse < (pulse + 150) && isPulse > (pulse - 150)) {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Correct!");
            }
        } else {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Incorrect");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    public int currentScene = 0;
    public GameObject[] scenes;
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach (GameObject scene in scenes) {
            scene.SetActive(i == currentScene ? true : false);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            scenes[currentScene].SetActive(false);
            currentScene++;
            scenes[currentScene].SetActive(true);
        }
        if (currentScene >= 4) {
            // go to main menu
        }
    }
}

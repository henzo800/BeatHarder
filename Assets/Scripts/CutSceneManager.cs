using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CutSceneManager : MonoBehaviour
{
    public int currentScene = 0;
    public GameObject[] scenes;
    // Start is called before the first frame update
    void Start()
    {

    }
    void DisableAllScenes() {
        foreach(GameObject scene in scenes){
            scene.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            DisableAllScenes();
            currentScene++;
            scenes[currentScene].SetActive(true);
        }
        if (currentScene >= 4) {
            SceneController.instance.LoadScene("MainMenu");
        }
    }
}

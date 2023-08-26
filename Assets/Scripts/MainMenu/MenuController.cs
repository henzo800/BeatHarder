using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject levelSelectorContentRoot;
    public GameObject levelSelectorButton;
    public GameObject[] pages;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i) { 
            string name = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)); 
            GameObject button = Instantiate(levelSelectorButton, levelSelectorContentRoot.transform);
            button.name = name;
            button.GetComponentInChildren<TMP_Text>().text = name;
            button.GetComponent<Button>().onClick.AddListener(() => OnSceneButtonClick(button.name)) ;
        } 
    }

    public void OnPlay() {
        pages[0].SetActive(false);
        pages[1].SetActive(true);
    }
    public void OnHowTo(){

    }
    public void OnSettings(){

    }
    public void OnSceneButtonClick(string targetScene){
        SceneController.instance.LoadScene(targetScene);
    }
}

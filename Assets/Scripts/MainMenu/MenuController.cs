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
    public GameObject backButton;
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
    void DisableAllPages(){
        foreach(GameObject page in pages){
            page.SetActive(false);
        }
    }
    public void OnPlay() {
        DisableAllPages();
        backButton.SetActive(true);
        pages[1].SetActive(true);
        
    }
    public void OnHowTo(){
        DisableAllPages();
        backButton.SetActive(true);
        pages[2].SetActive(true);
    }
    public void OnSettings(){
        DisableAllPages();
        backButton.SetActive(true);
        pages[3].SetActive(true);

    }
    public void OnSceneButtonClick(string targetScene){
        SceneController.instance.LoadScene(targetScene);
    }
    public void OnBack() {
        DisableAllPages();
        pages[0].SetActive(true);
        backButton.SetActive(false);
    }
}

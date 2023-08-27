using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    string NextScene = null;
    string currentScene = null;
    public GameObject LoadScreenPanel;
    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        LoadScreenPanel.SetActive(false);
        //Transition to IntroCutscene
        LoadScene("IntroCutScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string SceneName){
        NextScene = SceneName;
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine(){
        //Show Load panel
        LoadScreenPanel.SetActive(true);
        if(currentScene != null){
            AsyncOperation LastSceneUnload = SceneManager.UnloadSceneAsync(currentScene);
            while (!LastSceneUnload.isDone)
            {
                yield return null;
            }
        }

        AsyncOperation NextSceneLoad = SceneManager.LoadSceneAsync(NextScene, LoadSceneMode.Additive);
        
        while (!NextSceneLoad.isDone)
        {
            yield return null;
        }
        //Hide load screen
        LoadScreenPanel.SetActive(false);
        currentScene = NextScene;
        NextScene = null;

    }
}

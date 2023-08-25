using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [System.Serializable]
    public struct SceneData{
        public string Name;
    }
    public List<SceneData> SceneList;
    string NextScene = null;
    public GameObject LoadScreenPanel;
    // Start is called before the first frame update
    void Start()
    {
        LoadScreenPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LoadScene(string SceneName){
        NextScene = SceneName;
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine(){
        //Show Load panel
        LoadScreenPanel.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextScene);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        //Hide load screen
        LoadScreenPanel.SetActive(false);
        NextScene = null;

    }
}

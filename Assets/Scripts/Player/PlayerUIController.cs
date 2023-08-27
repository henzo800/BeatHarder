using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public static PlayerUIController instance;
    public GameObject TopIncoming;
    public GameObject DownIncoming;

    public Image health;
    public GameObject deathScreen;
    public GameObject winScreen;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {  
        if(GameManager.instance != null){
            TopIncoming.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f,
            Screen.height/2 - Screen.height/2*(((GameManager.instance.audioSource.time*1000) % (GameManager.instance.beatLength)) / GameManager.instance.beatLength)
            ,0f);

            DownIncoming.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f,
            - Screen.height/2 + Screen.height/2*(((GameManager.instance.audioSource.time*1000) % (GameManager.instance.beatLength)) / GameManager.instance.beatLength)
            ,0f);
        }else{
            TopIncoming.SetActive(false);
            DownIncoming.SetActive(false);
        }

        // Set health bar
        float bossHealthPercent = BossController.instance.getHealth() / BossController.instance.characterData.HEALTH;
        health.fillAmount = bossHealthPercent;
    }
    public void Death() {
        deathScreen.SetActive(true);
        PlayerController.instance.isControllable = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(deathWait());
    }
    IEnumerator deathWait(){
        yield return new WaitForSeconds(3);
        SceneController.instance.LoadScene("MainMenu");
    }
    public void Win() {
        winScreen.SetActive(true);
        PlayerController.instance.isControllable = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(winWait());
    }
    IEnumerator winWait(){
        yield return new WaitForSeconds(3);
        SceneController.instance.LoadScene("MainMenu");
    }
}

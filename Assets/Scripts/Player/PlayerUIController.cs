using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    public GameObject TopIncoming;
    public GameObject DownIncoming;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {  
        TopIncoming.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f,
        Screen.height/2 - Screen.height/2*(((GameManager.instance.audioSource.time*1000) % (GameManager.instance.beatLength)) / GameManager.instance.beatLength)
        ,0f);

        DownIncoming.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f,
        - Screen.height/2 + Screen.height/2*(((GameManager.instance.audioSource.time*1000) % (GameManager.instance.beatLength)) / GameManager.instance.beatLength)
        ,0f);
    }
}

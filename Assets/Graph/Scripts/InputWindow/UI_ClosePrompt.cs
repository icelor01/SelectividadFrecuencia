using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ClosePrompt : MonoBehaviour {

    public Button OKBtn;
    public GameObject feedback;

    // Use this for initialization
    void Start () {
       OKBtn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        feedback.SetActive(false);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class PlayAfterTutorial : MonoBehaviour
{

    public Button playButton;

    // Start is called before the first frame update
    void Awake()
    {
        playButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.manager.completed_tutorial)
        {
            playButton.interactable = true;
        }
    }
}

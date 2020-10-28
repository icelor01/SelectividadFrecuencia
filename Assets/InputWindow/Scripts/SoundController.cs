using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
  
    public Button CustomButton;
    public GameObject SoundImage;
    public GameObject NoSoundImage;


    void Awake()
    {
        CustomButton.onClick.AddListener(delegate { CustomButton_OnClick(); });
        /*
        if (GameManager_tutorial.manager.SoundOn == true)
        {
            SoundImage.SetActive (true);
            NoSoundImage.SetActive(false);

        }
        else {
            SoundImage.SetActive (false);
            NoSoundImage.SetActive(true);
        }
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Handle the onClick event
    public void CustomButton_OnClick()
    {
        //soundcontroller.TurnSoundOff();

         GameManager.manager.SoundControl();

        // Image
        if (GameManager.manager.SoundOn == true)
        {
            SoundImage.SetActive(true);
            NoSoundImage.SetActive(false);

        }
        else
        {
            SoundImage.SetActive(false);
            NoSoundImage.SetActive(true);
        }


    }



}

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
        CustomButton.onClick.AddListener(delegate { OnClick(); });
    }

    void Start()
    {
        bool on = GameManager.manager.IsSoundOn();
        SoundImage.SetActive(on);
        NoSoundImage.SetActive(!on);

        GameManager.manager.PlaceMusicButton(GetComponent<Transform>());
    }

    //Handle the onClick event
    public void OnClick()
    {
        GameManager.manager.ToggleSound();

        bool on = GameManager.manager.IsSoundOn();
        SoundImage.SetActive(on);
        NoSoundImage.SetActive(!on);
    }
}

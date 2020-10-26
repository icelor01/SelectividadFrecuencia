using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Music : MonoBehaviour
{
    static Music instance = null;
    public AudioSource Backsound;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    public void backsoundOnOff()
    {
        AudioSource bgsound = Backsound.GetComponent<AudioSource>();
        bgsound.mute = !bgsound.mute;
    }
}

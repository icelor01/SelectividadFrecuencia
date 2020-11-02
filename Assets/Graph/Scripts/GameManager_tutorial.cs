using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager_tutorial : MonoBehaviour {

    public static GameManager_tutorial manager; //instancia de GameManager2
                                                 // Use this for initialization
    public GameObject clip;
    //public GameObject soundButton;
    //public Button soundButton;
    AudioSource bgsound;
    float currentMusicTime;
    public bool SoundOn=true; //true=SoundOn false=SoundOff
    bool previousMusicPlaying=true; // variable para saber si sonaba la musica en la escena anterior
    public GameObject SoundButton;
    public GameObject SoundImage;
    public GameObject NoSoundImage;
    public bool solution_isCorrect = false;
    

    void Awake () {
        //Check if instance already exists
        if (manager == null) { 

            //if not, set instance to this
            manager = this;
        }

        //If instance already exists and it's not this:
        else if (manager != this) { 
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        }
        
        string activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "Menu")
            Debug.Log("Estoy en escena Menu");
        {
            if (clip != null) {
                Destroy(clip);
                Debug.Log("Y destruyo el clip");
            }
            else { 
            clip = GameObject.FindGameObjectWithTag("MenuMusic");
                Debug.Log("Pongo clip de MenuMusic");
            }
         }
        if (activeScene== "Tutorial1") { 
        clip = GameObject.FindGameObjectWithTag("TutorialMusic");
        DontDestroyOnLoad(clip); //Necesario para que no deje de reproducir la musica
        }      
       

    }


    public void Update()    {
        string activeScene = SceneManager.GetActiveScene().name;
        AudioSource audio;

        if (activeScene == "Menu")
            
        {

            // Si estamos en la escena Menu, hay que revisar que no esté sonando la música de otros reproductores
            //como podría ser la de TutorialMusic 
            try {

                // Si hay TutorialMusic, lo destruimos
                if (clip.tag == "TutorialMusic") {
                    Destroy(GameObject.FindGameObjectWithTag("TutorialMusic"));
                }
               
                  }
            catch {

                // Si no hay TutorialMusic, activamos MenuMusic
                clip = GameObject.FindGameObjectWithTag("MenuMusic");
                audio = clip.GetComponent<AudioSource>();
                Debug.Log("Suena MenuMusic");
                previousMusicPlaying = SoundOn;
            };

            // Si la música está sonando
           

        }

        if (activeScene == "Tutorial1")
        {

            //Intento de mejora: Si no sonaba la música en la escena anterior, que siga sin sonar
            if (SoundOn == false & previousMusicPlaying == false)
            {
                // Apagamos la música
                TurnSoundOff();
                SoundButton = GameObject.FindGameObjectWithTag("SoundButton");
                SoundImage = SoundButton.transform.Find("ImagenSonido").gameObject;
                NoSoundImage = SoundButton.transform.Find("ImagenSinSonido").gameObject;
                SoundImage.SetActive(false);
                NoSoundImage.SetActive(true);
                previousMusicPlaying = true;
            }
            else
            {

            

            GameObject[] gameObjects;
            gameObjects = GameObject.FindGameObjectsWithTag("TutorialMusic");
            // Si estamos en la escena Menu, hay que revisar que no esté sonando la música de otros reproductores
            //como podría ser la de TutorialMusic procedente de la escena 2 (dos reproductores)
           
            
                if (gameObjects.Length>1)
                {
                // Si hay dos TutorialMusic, debemos destruir un reproductor  
                // Ver como destruir un reproductor
                Destroy(gameObjects[1]);
                }
                
                // Si no hay dos TutorialMusic, activamos TutorialMusic
                clip = GameObject.FindGameObjectWithTag("TutorialMusic");
                audio = clip.GetComponent<AudioSource>();
                Debug.Log("Suena TutorialMusic");
                DontDestroyOnLoad(clip); //Necesario para que no deje de reproducir la musica

              
            }
        }

        if (activeScene == "Escena1")
        {
            // Si estamos en la Escena 1, hay que revisar que no esté sonando la música de otros reproductores
            //como podría ser la de TutorialMusic o la de Escena 2
            try
            {

                // Si hay TutorialMusic, lo destruimos
                if (clip.tag == "TutorialMusic")
                {
                    Destroy(GameObject.FindGameObjectWithTag("TutorialMusic"));
                }

            }
            catch
            {

                try
                {

                    // Si hay Scene2Music, lo destruimos
                    if (clip.tag == "Scene2Music")
                    {
                        Destroy(GameObject.FindGameObjectWithTag("Scene2Music"));
                    }

                }
                catch
                {

                    // Si no hay TutorialMusic ni Scene2, activamos Scene1Music
                    clip = GameObject.FindGameObjectWithTag("Scene1Music");
                    audio = clip.GetComponent<AudioSource>();
                    Debug.Log("Suena Scene1Music");
                };

                // Si no hay TutorialMusic ni Scene2, activamos Scene1Music
                clip = GameObject.FindGameObjectWithTag("Scene1Music");
                audio = clip.GetComponent<AudioSource>();
                Debug.Log("Suena Scene1Music");
             };

        
        }

    }

    private bool FindGameObjectsWithTag(string v)
    {
        throw new NotImplementedException();
    }

    public bool IsSoundOn() {
        return SoundOn;
    }


    public void SoundControl()
    {
        AudioSource bgsound = clip.GetComponent<AudioSource>();

        // Si la música está sonando
        if (IsSoundOn()) {
            // Apagamos la música
            bgsound.mute = true;
            SoundOn = false;
        }
        else {
            // Si no esta sonando, la activamos
            bgsound.mute = false;
            SoundOn = true;
        }

    }


    public void TurnSoundOn() {
       AudioSource bgsound = clip.GetComponent<AudioSource>();
        if (bgsound.mute == false)
        {
            bgsound.mute = true;
        }
        else
        {
            bgsound.mute = false;
        }
    }

    public void TurnSoundOff() {
        AudioSource bgsound = clip.GetComponent<AudioSource>();

        if (bgsound.mute == true)
        {
            bgsound.mute = false;
        }
        else
        {
            bgsound.mute = true;
        }
    }


}

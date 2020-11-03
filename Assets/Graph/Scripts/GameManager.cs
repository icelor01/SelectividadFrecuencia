using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using AssetPackage;

//Clase estática para acceder desde cualquier escena

public class GameManager : MonoBehaviour { 
    
    public Text ScoreT;
    public bool solutionIsCorrect = false;
    public bool tutorial_solutionIsCorrect = false;
    public static GameManager manager; //instancia de GameManager
    
    public const string octaveServerEndpoint = "http://gin.fdi.ucm.es:8080/f/";
    //public const string octaveServerEndpoint = "http://localhost:5000/f/";

    public GameObject clip;
    //public GameObject soundButton;
    //public Button soundButton;
    AudioSource bgsound;
    float currentMusicTime;
    public bool SoundOn = true; //true=SoundOn false=SoundOff
    bool previousMusicPlaying = true; // variable para saber si sonaba la musica en la escena anterior
    public GameObject permanente;
    public GameObject SoundButton;
    public GameObject SoundImage;
    public GameObject NoSoundImage;
    public bool completed_tutorial = false;
    public GameObject CheckImage;
    public int score;

    void updateReferences(Scene scene, LoadSceneMode mode) {

        string activeScene = scene.name;

        //permanente = GameObject.FindWithTag("Permanente");
        permanente = GameObject.Find("CanvasPermanente");
        AudioSource bgsound;

        switch (activeScene) {
            case "Menu":
            {
                score = 0;
                /*
                if (clip != null)
                {
                    Destroy(clip);
                    Debug.Log("Y destruyo el clip");
                }
                else
                {
                    clip = GameObject.FindWithTag("MenuMusic");
                    Debug.Log("Pongo clip de MenuMusic");
                }

                Debug.Log("Estoy en escena Menu");
                */
                try
                {
                    // Si hay TutorialMusic, lo destruimos
                    if (clip.tag == "TutorialMusic")
                    {
                        Destroy(GameObject.FindWithTag("TutorialMusic"));
                    }
                }
                catch
                {
                    // Si no hay TutorialMusic, activamos MenuMusic
                    clip = GameObject.FindWithTag("MenuMusic");
                    bgsound = clip.GetComponent<AudioSource>();
                    Debug.Log("Suena MenuMusic");
                    previousMusicPlaying = SoundOn;
                };

                    //CheckImage = GameObject.Find("CanvasPermanente/CheckImage");
                    CheckImage= permanente.transform.Find("CheckImage").gameObject;
                    if (completed_tutorial == true)
                    {
                        CheckImage.SetActive(true);
                    }
                    else
                    {
                        CheckImage.SetActive(false);
                    }

                    break;
            }
            case "Tutorial1":
            {                
                if (SoundOn == false & previousMusicPlaying == false)
                {
                    // Apagamos la música
                    TurnSoundOff();
                    SoundButton = GameObject.FindWithTag("SoundButton");
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

                    if (gameObjects.Length > 1)
                    {
                        // Si hay dos TutorialMusic, debemos destruir un reproductor  
                        // Ver como destruir un reproductor
                        Destroy(gameObjects[1]);
                    }

                    // Si no hay dos TutorialMusic, activamos TutorialMusic
                    clip = GameObject.FindWithTag("TutorialMusic");
                    bgsound = clip.GetComponent<AudioSource>();
                    Debug.Log("Suena TutorialMusic");
                    DontDestroyOnLoad(clip); //Necesario para que no deje de reproducir la musica
                }
                break;
            }
            case "Tutorial4a":
            {
                    try
                    {
                        // Si hay TutorialMusic, lo destruimos
                        if (clip.tag == "TutorialMusic")
                        {
                            Destroy(GameObject.FindWithTag("TutorialMusic"));
                        }
                    }
                    catch
                    {
                        // Si no hay TutorialMusic, activamos TutorialMusic2
                        clip = GameObject.FindWithTag("TutorialMusic2");
                        bgsound = clip.GetComponent<AudioSource>();
                        Debug.Log("Suena TutorialMusic2");
                        previousMusicPlaying = SoundOn;
                        DontDestroyOnLoad(clip);
                    };
                    break;
            } 
            case "Escena1": InitLevel("Scene1Music"); break;
            case "Escena2": InitLevel("Scene2Music"); break;
            case "Escena3": InitLevel("Scene3Music"); break;

            case "FinalScene":
                {
                    try
                    {
                        // Si hay TutorialMusic, lo destruimos
                        if (clip.tag == "Scene3Music")
                        {
                            Destroy(GameObject.FindWithTag("Scene3Music"));
                        }
                    }
                    catch
                    {
                        // Si no hay TutorialMusic, activamos TutorialMusic2
                        clip = GameObject.FindWithTag("FinalSceneMusic");
                        bgsound = clip.GetComponent<AudioSource>();
                        Debug.Log("Suena FinalSceneMusic");
                        previousMusicPlaying = SoundOn;
                        
                    };
                    break;
                }
        }


    }

    private void InitLevel(string music) {
            GameObject creditos = permanente.transform.Find("Creditos").gameObject;
            ScoreT= creditos.GetComponent<Text>();
            ScoreT.text = "Créditos: " + score;
            if (clip != null)
            {
                Destroy(clip);
            }
            else
            {
                clip = GameObject.FindWithTag(music);
                DontDestroyOnLoad(clip);
        }
    }

    void Awake()
    {
        //Check if instance already exists
        if (manager != null) return;

        manager = this;
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        // ver https://raw.githubusercontent.com/e-ucm/unity-tracker/master/Tracker.cs
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.sceneLoaded += updateReferences;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if ( ! Tracker.T.Active) {
            Tracker.T.Start();
        }
        string name = scene.name;
        Debug.Log("Scene Loaded " + name);
        Tracker.T.Completable.Initialized(name);
        Tracker.T.Flush();
    }
    void OnSceneUnloaded(Scene scene) {
        string name = scene.name;
        Debug.Log("Scene Unloaded " + name);
        Tracker.T.Completable.Completed(name);
        Tracker.T.Flush();
    }

    public void TrackSliderValue(string key, string value) {
        Tracker.T.setVar(key, value);
        Tracker.T.GameObject.Interacted(key);
    }

    public void Start()    {
    }

    public void AddScore(int add) {
        score += add;
        if (ScoreT != null) {
            ScoreT.text = "Créditos: " + score;
        }
        Tracker.T.setScore(score);
        Tracker.T.setCompletion(true); // because we only set score when we win
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public bool IsSoundOn()
    {
        return SoundOn;
    }


    public void SoundControl()
    {
        AudioSource bgsound = clip.GetComponent<AudioSource>();

        // Si la música está sonando
        if (IsSoundOn())
        {
            // Apagamos la música
            bgsound.mute = true;
            SoundOn = false;
        }
        else
        {
            // Si no esta sonando, la activamos
            bgsound.mute = false;
            SoundOn = true;
        }
    }

    public void TurnSoundOn()
    {
        AudioSource bgsound = clip.GetComponent<AudioSource>();
        bgsound.mute = ! bgsound.mute;
    }

    public void TurnSoundOff()
    {
        AudioSource bgsound = clip.GetComponent<AudioSource>();
        bgsound.mute = ! bgsound.mute;
    }
}

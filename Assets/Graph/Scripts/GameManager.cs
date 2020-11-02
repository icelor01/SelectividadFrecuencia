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

    void Awake()
    {
        //Check if instance already exists
        if (manager == null)
        {

            //if not, set instance to this
            manager = this;
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);
        }

       
        else
        {
            // If instance already exists, exit
            return;
        }

        // ver https://raw.githubusercontent.com/e-ucm/unity-tracker/master/Tracker.cs
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        string activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == "Menu")
            Debug.Log("Estoy en escena Menu");
            permanente = GameObject.FindGameObjectWithTag("Permanente");
        {
            if (clip != null)
            {
                Destroy(clip);
                Debug.Log("Y destruyo el clip");
            }
            else
            {
                clip = GameObject.FindGameObjectWithTag("MenuMusic");
                Debug.Log("Pongo clip de MenuMusic");
            }
        }
        if (activeScene == "Tutorial1")
        {
            permanente = GameObject.FindGameObjectWithTag("Permanente");
            clip = GameObject.FindGameObjectWithTag("TutorialMusic");
            DontDestroyOnLoad(clip); //Necesario para que no deje de reproducir la musica
        }

        if (activeScene == "Escena1")
        {
            permanente = GameObject.FindGameObjectWithTag("Permanente");
            GameObject creditos = permanente.transform.Find("Creditos").gameObject;
            ScoreT.text = "Créditos: " + score;
            clip = GameObject.FindGameObjectWithTag("Scene1Music");
            DontDestroyOnLoad(clip); //Necesario para que no deje de reproducir la musica
        }
        if (activeScene == "Escena2")
        {
            permanente = GameObject.FindGameObjectWithTag("Permanente");
            GameObject creditos = permanente.transform.Find("Creditos").gameObject;
            ScoreT.text = "Créditos: " + score;
            clip = GameObject.FindGameObjectWithTag("Scene2Music");
            DontDestroyOnLoad(clip); //Necesario para que no deje de reproducir la musica
        }
        if (activeScene == "Escena3")
        {
            permanente = GameObject.FindGameObjectWithTag("Permanente");
            GameObject creditos = permanente.transform.Find("Creditos").gameObject;
            ScoreT.text = "Créditos: " + score;
            clip = GameObject.FindGameObjectWithTag("Scene3Music");
            DontDestroyOnLoad(clip); //Necesario para que no deje de reproducir la musica
        }

        CheckImage = GameObject.FindGameObjectWithTag("CheckImage");
        if (completed_tutorial == true)
        {
            CheckImage.SetActive(true);
        }

        else
        {
            CheckImage.SetActive(false);
        }
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


    public void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        AudioSource audio;

        if (activeScene == "Menu")
        {
            permanente = GameObject.FindGameObjectWithTag("Permanente");
            CheckImage = permanente.transform.Find("CheckImage").gameObject;
            //CheckImage = GameObject.FindGameObjectWithTag("CheckImage");
            if (completed_tutorial == true)
            {
                
                CheckImage.SetActive(true);
            }

            else {
                CheckImage.SetActive(false);
            }
            // Si estamos en la escena Menu, hay que revisar que no esté sonando la música de otros reproductores
            //como podría ser la de TutorialMusic 
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

                if (gameObjects.Length > 1)
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

            //ScoreT = GameObject.FindGameObjectWithTag("Creditos").GetComponent<Text>();
            permanente = GameObject.FindGameObjectWithTag("Permanente");
            GameObject creditos = permanente.transform.Find("Creditos").gameObject;
            ScoreT = creditos.GetComponent<Text>();
            ScoreT.text = "Créditos: " + score;
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

        if (activeScene == "Escena2" || activeScene == "Escena3")
        {
            //ScoreT = GameObject.FindGameObjectWithTag("Creditos").GetComponent<Text>();
            permanente = GameObject.FindGameObjectWithTag("Permanente");
            GameObject creditos = permanente.transform.Find("Creditos").gameObject;
            ScoreT = creditos.GetComponent<Text>();
            ScoreT.text = "Créditos: " + score;
            
        }

        }

    private bool FindGameObjectsWithTag(string v)
    {
        throw new NotImplementedException();
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

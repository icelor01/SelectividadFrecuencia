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
    public static GameManager manager; 
    
    public const string octaveServerEndpoint = "http://gin.fdi.ucm.es:8080/f/";
    //public const string octaveServerEndpoint = "http://localhost:5000/f/";

    public bool SoundOn = true; //true=SoundOn false=SoundOff
    public GameObject permanente;
    public bool completed_tutorial = false;
    public GameObject CheckImage;
    public int score;

    public AudioClip menuMusic;
    public AudioClip tutorialMusic1;
    public AudioClip tutorialMusic2;
    public AudioClip exerciseMusic1;
    public AudioClip exerciseMusic2;
    public AudioClip exerciseMusic3;
    public AudioClip endGameMusic;

    private AudioSource audioSource;

    public int soundPosX;
    public int soundPosY;

    void updateReferences(Scene scene, LoadSceneMode mode) {

        string activeScene = scene.name;
        permanente = GameObject.Find("CanvasPermanente");

        switch (activeScene) {
            case "Menu":
            {
                SetMusic(menuMusic);
                score = 0;
                CheckImage = permanente.transform.Find("CheckImage").gameObject;
                CheckImage.SetActive(completed_tutorial);
                break;
            }
            case "Tutorial1":
            {                
                SetMusic(tutorialMusic1);
                break;
            }
            case "Tutorial4a":
            {
                SetMusic(tutorialMusic2);
                break;
            } 
            case "Escena1":
            {
                SetMusic(exerciseMusic1);
                InitLevel(); 
                break;
            }
            case "Escena2":
            {
                SetMusic(exerciseMusic2);
                InitLevel(); 
                break;
            }
            case "Escena3": 
            {
                SetMusic(exerciseMusic3);
                InitLevel(); 
                break;
            }
            case "FinalScene":
            {
                SetMusic(endGameMusic);
                break;
            }
        }
    }

    public void GoToScene(string sceneName) {
        var parameters = new LoadSceneParameters(LoadSceneMode.Single);
        SceneManager.LoadScene(sceneName, parameters);
    }

    public void Quit() {
        Tracker.T.Exit();
        Application.Quit();
    }

    private void SetMusic(AudioClip music) {
        if (audioSource.clip != music) {
            audioSource.clip = music;
            if ( ! audioSource.mute) {
                audioSource.Play();
            }
        }    
    }

    private void InitLevel() {
        GameObject creditos = permanente.transform.Find("Creditos").gameObject;
        ScoreT= creditos.GetComponent<Text>();
        ScoreT.text = "Créditos: " + score;
    }

    void Awake()
    {
        //Check if instance already exists
        if (manager != null) return;
        manager = this;
      
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        Debug.Log("Audio source is " + audioSource);

        // ver https://raw.githubusercontent.com/e-ucm/unity-tracker/master/Tracker.cs
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.sceneLoaded += updateReferences;
    }

    void Start() {        
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
        return audioSource == null ||  ! audioSource.mute;
    }


    public void ToggleSound()
    {
        audioSource.mute = ! audioSource.mute;
    }
}

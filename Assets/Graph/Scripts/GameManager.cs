using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using AssetPackage;

//Clase estática para acceder desde cualquier escena

public class GameManager : MonoBehaviour { 
    public int score;
    public Text ScoreT;
    public bool solution_isCorrect = false;
    public static GameManager instance;

    private void Awake()    {
        DontDestroyOnLoad(this);
        
        if (instance != null) {
            return;
        }
        instance = this; // only done once

        // ver https://raw.githubusercontent.com/e-ucm/unity-tracker/master/Tracker.cs
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
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
}

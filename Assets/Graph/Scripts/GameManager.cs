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
    
    private string ID;

    private static TrackerAsset tracker;

    public static GameManager instance;

    private void Awake()    {
        DontDestroyOnLoad(this);
        
        if (instance != null) {
            Debug.LogError("Tried to instantiate a vile copy of GameManager");
            Destroy(gameObject);
            return;
        }
        instance = this; // only done once

        Debug.Log("Initializing tracking...", this);
        tracker = TrackerAsset.Instance;
        tracker.Bridge = new UnityBridge();        
        ID = "" + Random.Range(10000, 99999);
        TrackerAssetSettings settings = new TrackerAssetSettings()
        {
            LogFile = "trazas_" + ID + ".log",
            StorageType = TrackerAsset.StorageTypes.local,
            TraceFormat = TrackerAsset.TraceFormats.xapi,
            BackupStorage = true
        };
        tracker.Settings = settings;

        tracker.Start();
        Debug.Log("Tracking parece funcionar... c:" +  tracker.Connected + " x:" + tracker.Active);

        tracker.Alternative.Selected("AlternativeID", "SelectedAnswer");
        tracker.Flush();

        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded<Scene> (Scene scene, LoadSceneMode mode) {
        Debug.Log("Scene Loaded " + scene);
        tracker.Completable.Initialized("" + scene);
        tracker.Flush();
    }
    void OnSceneUnloaded<Scene> (Scene scene) {
        Debug.Log("Scene Unloaded " + scene);
        tracker.Completable.Completed("" + scene);
        tracker.Flush();
    }

    public void Start()    {
    }

    public void AddScore(int add) {
        score += add;
        ScoreT.text = "Créditos: " + score;
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using AssetPackage;

//Clase estática para acceder desde cualquier escena

public class GameManager : MonoBehaviour { 

    public static GameManager instance; //instancia de GameManeger
    /*
    public int vida;
    public int maxVida = 3;
    public GameObject[] corazones;
    */
    public int score;
    public Text ScoreT;
    public bool solution_isCorrect = false;
   
    private void Awake()    {
        //print("Awake GameManager");
        instance = this;
        ScoreT.text = "Créditos: " + score;

        TrackerAsset.Instance.Settings = new TrackerAssetSettings();
        Debug.Log("Tracking parece funcionar...", this);

        // You can use Tracker via Singleton:
        TrackerAsset.Instance.Bridge = new UnityBridge();
        TrackerAsset.Instance.Start ();

        TrackerAsset.Instance.Alternative.Selected("AlternativeID", "SelectedAnswer");
        TrackerAsset.Instance.Flush();

        //You can create your own Tracker instance and manage it yourself.
        TrackerAsset player2tracker = new TrackerAsset();
        player2tracker.Settings = new TrackerAssetSettings();
        player2tracker.Bridge = new UnityBridge();
        player2tracker.Start ();

        player2tracker.Alternative.Selected("AlternativeID", "SelectedAnswer2");
        player2tracker.Flush();        
    }

    public void AddScore(int add) {
        score += add;
        ScoreT.text = "Créditos: " + score;
      }


}

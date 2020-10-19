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

        TrackerAsset.Instance.Settings = new TrackerAssetSettings(); // New TrackerAsset instance
        Debug.Log("Tracking parece funcionar...", this);
        Debug.Log("El host o IP del servidor de analiticas es: " + TrackerAsset.StorageTypes.net);

        // You can use Tracker via Singleton:
        TrackerAsset.Instance.Bridge = new UnityBridge(); //Bridge implementation
        TrackerAsset.Instance.Start (); // Tracker start

        TrackerAsset.Instance.Alternative.Selected("AlternativeID", "SelectedAnswer");
        TrackerAsset.Instance.Flush();

        //You can create your own Tracker instance and manage it yourself.
        TrackerAsset player2tracker = new TrackerAsset();
        player2tracker.Settings = new TrackerAssetSettings();
        player2tracker.Bridge = new UnityBridge(); //Bridge implementation
        player2tracker.Start (); // Tracker start

        player2tracker.Alternative.Selected("AlternativeID", "SelectedAnswer2");
        player2tracker.Flush();

        /*
        //Tracker configuration
        String domain = "https://rage.e-ucm.es/";

        TrackerAssetSettings tracker_settings = new TrackerAssetSettings()
        {
            Host = domain,
            TrackingCode = "OBTAINED_FROM_SERVER",
            BasePath = "/api",
            Port = 334,
            Secure = domain.Split('/')[0] == "https:",
            StorageType = TrackerAsset.StorageTypes.net,
            TraceFormat = TrackerAsset.TraceFormats.xapi,
            BackupStorage = true,
            LoginEndpoint = trackerConfig.getLoginEndpoint() ?? "login",
            StartEndpoint = trackerConfig.getStartEndpoint() ?? "proxy/gleaner/collector/start/{0}",
            TrackEndpoint = trackerConfig.getStartEndpoint() ?? "proxy/gleaner/collector/track",
            UseBearerOnTrackEndpoint = trackerConfig.getUseBearerOnTrackEndpoint()
        };

        TrackerAsset.Instance.Settings = tracker_settings
        */

        /*Tracker Login and start (synchronous)
         * //Log in the student BEFORE starting the tracker; you can also retrieve this from, say, a configuration file in the filesystem
            String username = "student", password = "123456";
            TrackerAsset.Instance.Login(username, password);
            //Start the tracker before sending traces.
            TrackerAsset.Instance.Start();
         */

        /*Tracker Login and start (asynchronous)
         * ////Log in the student BEFORE starting the tracker
        String username = "student", password = "123456";

        TrackerAsset.Instance.LoginAsync(username, password, logged => {
        if(logged){
        // Code for when the login is successful
        } else {
        // Code for login failed
        }
        });
        TrackerAsset.Instance.StartAsync(() => {
        // Code to be executed after start
        });
        */

    }

    public void AddScore(int add) {
score += add;
ScoreT.text = "Créditos: " + score;
}


}

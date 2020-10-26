using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Métodos que no tienen tiempo, llamadas

public class Escenas : MonoBehaviour {

    //Lo hacemos singleton para poder acceder a él  desde cualquier sitio
    public static Escenas instance;
    private Scene scene;


    public void GoToScene (string sceneName) {

        var parameters = new LoadSceneParameters(LoadSceneMode.Single);
        scene= SceneManager.LoadScene(sceneName, parameters);

       DontDestroyOnLoad(GameManager_tutorial.manager);
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("TutorialMusic"));

        //DontDestroyOnLoad(GameManager.instance);
        /*
        //En la Escena 2 (id=6), no queremos destruir el GameManager anterior: GameManager
        if (sceneIndex == 6)
        {
            DontDestroyOnLoad(GameManager.instance);
        }
        //En la Escena 3 (id=7), no queremos destruir el GameManager anterior: GameManager2
        else if (sceneIndex == 7)
        {
            DontDestroyOnLoad(GameManager2.instance2);
        }
        else { }
        */
    }

    private void Awake() {
        instance = this;
    }
	
	public void Quit () {
        Application.Quit();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Métodos que no tienen tiempo, llamadas

public class Escenas : MonoBehaviour {

    //Lo hacemos singleton para poder acceder a él  desde cualquier sitio
    public static Escenas instance;

	public void GoToScene (int sceneIndex) {

        SceneManager.LoadScene(sceneIndex);

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
    }

    private void Awake() {
        instance = this;
    }
	
	public void Quit () {
        Application.Quit();
	}
}

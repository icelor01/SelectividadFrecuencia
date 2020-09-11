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
	}

    private void Awake() {
        instance = this;
    }
	
	public void Quit () {
        Application.Quit();
	}
}

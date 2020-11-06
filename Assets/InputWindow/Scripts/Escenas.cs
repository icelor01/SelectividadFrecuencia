using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Métodos que no tienen tiempo, llamadas

public class Escenas : MonoBehaviour {
    public void GoToScene(string name) {
        GameManager.manager.GoToScene(name);
    }

	public void Quit () {
        GameManager.manager.Quit();
    }
}

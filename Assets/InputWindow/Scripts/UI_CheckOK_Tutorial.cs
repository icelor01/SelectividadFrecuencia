using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_CheckOK_Tutorial : MonoBehaviour
{

    public Button OKBtn;
    public int puntos_tutorial;

    // Use this for initialization
    void Start()
    {
        OKBtn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {

        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (GameManager.manager.solutionIsCorrect == true)
        {
            Debug.Log("Solution scene: " + sceneName + "is correct");
            GameManager.manager.AddScore(puntos_tutorial);
            // Debemos cambiar a la escena siguiente a no ser que estemos en la escena ultima 7, que regresamos al menú principal id.0

            if (sceneName == "Tutorial4b")
            {
                Escenas.instance.GoToScene("Tutorial5a");
            }

            if (sceneName == "Tutorial5c")
            {
                GameManager.manager.completed_tutorial = true;
                Debug.Log("He completado el tutorial y voy al menu");
                Escenas.instance.GoToScene("Menu");
            }

            else
        {
                Debug.Log("Solution scene: " + sceneName + "is incorrect");
                puntos_tutorial -= 2;
            // Debemos mantenernos en la misma escena
            //do nothing
        }

    }
    }
}
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
        if (GameManager_tutorial.manager.solution_isCorrect == true)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            Debug.Log("Solution scene" + sceneName + "is correct");
            GameManager.manager.AddScore(puntos_tutorial);
            // Debemos cambiar a la escena siguiente a no ser que estemos en la escena ultima 7, que regresamos al menú principal id.0

            if (sceneName == "Tutorial4b")
            {
                Escenas.instance.GoToScene("Tutorial5a");
            }
           
        
        else
        {
            Debug.Log("Solution 1 is incorrect");
            puntos_tutorial -= 2;
            // Debemos mantenernos en la misma escena
            //do nothing
        }

    }
    }
}
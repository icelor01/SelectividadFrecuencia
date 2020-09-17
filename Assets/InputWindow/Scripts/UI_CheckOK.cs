using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_CheckOK : MonoBehaviour {

    public Button OKBtn;
    public int puntos;
    
    // Use this for initialization
    void Start()  {
        OKBtn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()  {
        if (GameManager.instance.solution_isCorrect == true)
        {
            int nextScene = 6;
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            Debug.Log("Solution scene"+ sceneName + "is correct");
            GameManager.instance.AddScore(puntos);
            // Debemos cambiar a la escena siguiente a no ser que estemos en la escena ultima 7, que regresamos al menú principal id.0
            if (sceneName == "Escena3")
            {
                Escenas.instance.GoToScene(0);
            }
            else{Escenas.instance.GoToScene(nextScene);
                nextScene += 1;
            }
        }
        else
        {
            Debug.Log("Solution 1 is incorrect");
            puntos -= 2;
            // Debemos mantenernos en la misma escena
            //do nothing
        }

    }
}
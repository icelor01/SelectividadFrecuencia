using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CheckOK : MonoBehaviour {

    public Button OKBtn;
    public int puntos = 20;
    
    // Use this for initialization
    void Start()  {
        OKBtn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()  {
        if (GameManager.instance.solution1_isCorrect == true)
        {
            Debug.Log("Solution 1 is correct");
            GameManager.instance.AddScore(puntos);
            // Debemos cambiar a la escena 3 al pulsar el boton OK
            Escenas.instance.GoToScene(3);
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
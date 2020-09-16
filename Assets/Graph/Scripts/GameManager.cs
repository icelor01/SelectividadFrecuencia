using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Clase estática para acceder desde cualquier escena

public class GameManager : MonoBehaviour { 

    public static GameManager instance; //instancia de GameManeger
    /*
    public int vida;
    public int maxVida = 3;
    public GameObject[] corazones;
    */
    public int score=0;
    public Text ScoreT;
    public bool solution1_isCorrect = false;
    public bool solution2_isCorrect = false;

    private void Awake()    {
        //print("Awake GameManager");
        instance = this;
       ScoreT.text = "Créditos: " + score;
    }

    public void AddScore(int add) {
        score += add;
        ScoreT.text = "Créditos: " + score;
      }


}

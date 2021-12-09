using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Clase estática para acceder desde cualquier escena

public class GameManager2 : MonoBehaviour
{

    public static GameManager2 instance2; //instancia de GameManager2

    public int score = 0;
    public Text ScoreT;
    public bool solution_isCorrect = false;

    private void Awake()
    {
        //int initial_score = GameManager.manager.score;
        //int initial_score = ProjectVars.Instance.score;
        //ScoreT.text = "Créditos: " + initial_score;
        instance2 = this;
    }

    public void AddScore(int add)
    {
        score += add;
        ScoreT.text = "Créditos: " + score;
    }


}

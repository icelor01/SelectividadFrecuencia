using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Clase estática para acceder desde cualquier escena

public class GameManager3 : MonoBehaviour
{

    public static GameManager3 instance3; //instancia de GameManager2

    public int score = 0;
    public Text ScoreT;
    public bool solution_isCorrect = false;

    private void Awake()
    {
        int initial_score = GameManager2.instance2.score;
        //int initial_score = ProjectVars.Instance.score;
        ScoreT.text = "Créditos: " + initial_score;
        instance3 = this;
    }

    public void AddScore(int add)
    {
        score += add;
        ScoreT.text = "Créditos: " + score;
    }


}

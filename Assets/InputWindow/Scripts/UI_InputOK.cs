using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_InputOK : MonoBehaviour {

    public Button OKButton;
    public Button okBtn1;
    public Button okBtn2;
    //private Button_UI cancelBtn;
    [SerializeField] public GameObject plotComponent;
    public GameObject feedbackOK;
    public GameObject feedbackNOK;
    private int xMin;
    private int xMax;
    private int n;
    private int xMin_solution;
    private int xMax_solution;
    
    // Use this for initialization
    void Update () {
        OKButton.onClick.AddListener(TaskOnClick1);
        //okBtn1.onClick.AddListener(TaskOnClick2);
        //okBtn2.onClick.AddListener(TaskOnClick3);

    }

      void TaskOnClick1()  {
        //Output this to console when ButtonOK is clicked
        //Comprobamos si los valores de xmin y max introducidos coinciden con los de la solucion esperada

        Plot plot = plotComponent.GetComponent<Plot>();
        Table table = plot.getTable();
        xMin = table.Getxmin();
        xMax = table.Getxmax();
        n = table.Getn();
        xMin_solution = table.Getxmin_solution();
        xMax_solution = table.Getxmax_solution();

        if (xMin_solution==xMin & xMax_solution == xMax) { 
        Debug.Log("Respuesta correcta");

            feedbackOK.SetActive(true);

        }
        else
        {
            feedbackNOK.SetActive(true);
            Debug.Log("Respuesta incorrecta");
            //respuestaIncorrecta.SetActive(true);
        }

    }

    void TaskOnClick2()
    {
        feedbackOK.SetActive(false);

    }

    void TaskOnClick3()
    {
        feedbackNOK.SetActive(false);

    }





}
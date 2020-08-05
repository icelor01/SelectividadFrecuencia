using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
//using TMPro;

public class UI_InputOK : MonoBehaviour {

    private static UI_InputOK instance;
    private Button_UI okBtn;
    private Button_UI cancelBtn;
    [SerializeField] public GameObject plotComponent;
    private int xMin;
    private int xMax;
    private int amplitude;
    //private int n;
    private int xMin_solution;
    private int xMax_solution;
    private int amplitude_solution;
    public GameObject feedbackOK;
    public GameObject feedbackNOK;
    public Slider sliderInstance;
    public GameObject feedbackText;

    // Use this for initialization
    private void Awake()
    {
        instance = this;
        Hide();
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        //feedbackText = transform.Find("FeedbackText").GetComponent<Text>();
    }

    // Use this for initialization
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            okBtn.ClickFunc();
        }
        /* if (Input.GetKeyDown(KeyCode.Escape))
         {
            cancelBtn.ClickFunc();
        }*/

    }


    private void Show(string inputString, Action onCancel, Action<string> onOk)
    {

        Plot plot = plotComponent.GetComponent<Plot>();
        Table table = plot.getTable();
        xMin = table.Getxmin();
        xMax = table.Getxmax();
        amplitude = table.GetAmplitude();
        //n = table.Getn();
        xMin_solution = table.Getxmin_solution();
        xMax_solution = table.Getxmax_solution();
        amplitude_solution = table.GetAmplitude_solution();

        if (sliderInstance.value == 1)
        {
            Debug.Log("Falta indicar si existe selectividad en frecuencia");
            //feedbackText.GetComponent<Text>= "Recuerda indicar si existe selectividad en frecuencia o no";
            //feedback = GameObject.Find("FeedbackOK");
            feedbackText.SetActive(true);

        }

        else if (xMin_solution == xMin & xMax_solution == xMax & amplitude_solution==amplitude & sliderInstance.value==2)
        {
            Debug.Log("Respuesta correcta");

            //feedback = GameObject.Find("FeedbackOK");
            feedbackOK.SetActive(true);

        }
        else
        {
            //feedback = GameObject.Find("FeedbackNOK");
            feedbackNOK.SetActive(true);
            Debug.Log("Respuesta incorrecta");
            //respuestaIncorrecta.SetActive(true);
        }




        gameObject.SetActive(true);


    

        okBtn.ClickFunc = () => {
                        Hide();
        };

        /*cancelBtn.ClickFunc = () => {
            Hide();
            onCancel();
        };*/

    }

    public void Hide()
    {
        feedbackOK.SetActive(false);
        feedbackNOK.SetActive(false);
        feedbackText.SetActive(false);
        gameObject.SetActive(false);
       
    }

 
    public static void Show_Static(string inputString, Action onCancel, Action<string> onOk)
    {
        instance.Show(inputString, onCancel, onOk);

     }



}
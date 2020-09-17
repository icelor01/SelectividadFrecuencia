using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
//using TMPro;

public class UI_InputOK2 : MonoBehaviour {

    private static UI_InputOK2 instance;
    private Button_UI okBtn;
    //private Button_UI cancelBtn;
    [SerializeField] public GameObject plotComponent;
    private int xMin;
    private int xMax;
    private int amplitude;
    //private int n;
    private int xMin_solution;
    private int xMax_solution;
    private int amplitude_solution;
    //public GameObject feedbackOK;
    //public GameObject feedbackNOK;
    public ToggleManager toggleManagerInstance;
    public Text feedbackText;

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
        xMin_solution = table.Getxmin_solution();
        xMax_solution = table.Getxmax_solution();
        amplitude_solution = table.GetAmplitude_solution();

        
        // Si no está seleccionado
        if (toggleManagerInstance.activeToggleid == 0)
        {
            Debug.Log("Falta indicar si existe selectividad en frecuencia");
            feedbackText.color = Color.blue;
            feedbackText.text= "Recuerda indicar si existe selectividad en frecuencia o no";
          
        }

        // Si la solución es correcta
        else if (((xMin >= xMin_solution-1) & (xMin <= xMin_solution + 1)) & ((xMax >= xMax_solution - 1) & (xMax <= xMax_solution + 1)) & ((amplitude >= amplitude_solution - 1) & (amplitude <= amplitude_solution + 1)) & toggleManagerInstance.activeToggleid == 2)
        {
            Debug.Log("Respuesta correcta");
            feedbackText.color = Color.green;
            feedbackText.text = "¡¡Muy bien!! Has ajustado correctamente el canal";
            GameManager2.instance2.solution_isCorrect = true;
            

        }
        // Si la solución es incorrecta
        else
        {
            feedbackText.color = Color.red;
            feedbackText.text = "¡Vaya! No has ajustado correctamente el canal. Vuelve a intentarlo o revisa las fórmulas";
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
        gameObject.SetActive(false);
       
    }

 
    public static void Show_Static(string inputString, Action onCancel, Action<string> onOk)
    {
        instance.Show(inputString, onCancel, onOk);

     }



}
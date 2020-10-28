using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
//using TMPro;

public class UI_InputOK_Tutorial : MonoBehaviour
{

    private static UI_InputOK_Tutorial instance;
    private Button_UI okBtn;
   [SerializeField] public GameObject plotComponent;
    private int xMin;
    private int xMax;
    private int amplitude;
    //Valores solucion para cada Ejercicio
    public int xMin_solution;
    public int xMax_solution;
    public int amplitude_solution;
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
     }


    private void Show(string inputString, Action onCancel, Action<string> onOk)
    {

        Plot plot = plotComponent.GetComponent<Plot>();
        Table table = plot.getTable();
        xMin = table.Getxmin();
        xMax = table.Getxmax();
        amplitude = table.GetAmplitude();


        // Si la solución es correcta
        if (((xMin >= xMin_solution - 1) & (xMin <= xMin_solution + 1)) & ((xMax >= xMax_solution - 1) & (xMax <= xMax_solution + 1)) & ((amplitude >= amplitude_solution - 1) & (amplitude <= amplitude_solution + 1)))
        {
            Debug.Log("Respuesta correcta");
            feedbackText.color = Color.green;
            feedbackText.text = "¡¡Muy bien!! Has ajustado correctamente el canal";
            GameManager.manager.solution_isCorrect = true;


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
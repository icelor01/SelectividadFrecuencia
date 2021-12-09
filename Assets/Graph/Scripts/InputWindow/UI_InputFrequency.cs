using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_InputFrequency : MonoBehaviour {

    private static UI_InputFrequency instance;
    private Button_UI okBtn;
    private Button_UI cancelBtn;
    private Text fc_text;
    private InputField Input_fc;
    [SerializeField] public GameObject plotComponent;


    // Use this for initialization
    private void Awake()
    {
        instance = this;
        Hide();
        okBtn = transform.Find("okBtn").GetComponent<Button_UI>();
        cancelBtn = transform.Find("cancelBtn").GetComponent<Button_UI>();
        InputField Input_fc = GameObject.Find("Input_fc").GetComponent<InputField>();
    }

    // Use this for initialization
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            okBtn.ClickFunc();
        }
         if (Input.GetKeyDown(KeyCode.Escape))
         {
            cancelBtn.ClickFunc();
        }

    }


    private void Show(string inputString, Action onCancel, Action<string> onOk)
    {
        gameObject.SetActive(true);
  
        //String ObjectsText = txt_Input.text;
        int characterLimit = 5;
        Input_fc = GameObject.Find("Input_fc").GetComponent<InputField>();
        Input_fc.characterLimit = characterLimit;
        InputField.ContentType FloatNumber = default(InputField.ContentType);
        Input_fc.contentType = FloatNumber;
       
        okBtn.ClickFunc = () => {
            InputField Input_fc = GameObject.Find("Input_fc").GetComponent<InputField>();
            Debug.Log("fc: " + Input_fc.text);
            //Pedir a plot dibujar con estos valores
            Plot plot = plotComponent.GetComponent<Plot>();
            Table table = plot.getTable();
            float fc = float.Parse(Input_fc.text);
            table.Changefc(fc);
            table.RequestData(plot);
            onOk(Input_fc.text);
            Hide();
        };

        cancelBtn.ClickFunc = () => {
            Hide();
            onCancel();
        };

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

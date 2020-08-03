using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class UI_InputFormula : MonoBehaviour {

        private static UI_InputFormula instance;
        private Button_UI cancelBtn;
        
        // Use this for initialization
        private void Awake()
        {
            instance = this;
            Hide();
            cancelBtn = transform.Find("cancelBtn").GetComponent<Button_UI>();
        }

        // Use this for initialization
        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
            {
                cancelBtn.ClickFunc();
            }
          }


        private void Show(string inputString, Action onCancel, Action<string> onOk)
        {

            gameObject.SetActive(true);
            
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
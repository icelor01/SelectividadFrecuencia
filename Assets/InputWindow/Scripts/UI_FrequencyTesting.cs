using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;

public class UI_FrequencyTesting : MonoBehaviour {

    private void Start()
    {

        transform.Find("m_FrequencyButton").GetComponent<Button_UI>().ClickFunc = () => {
            //inputSpan.Show(() => {
            UI_InputFrequency.Show_Static("", () => {
                // Clicked Cancel
                CMDebug.TextPopupMouse("Cancel!");
            }, (string inputText) => {
                // Clicked Ok
                CMDebug.TextPopupMouse("Ok: " + inputText);

            });
        };

    }
}

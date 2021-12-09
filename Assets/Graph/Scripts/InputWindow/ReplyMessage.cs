using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplyMessage : MonoBehaviour {

    public Text myReplyText;
    public Plot myChannel;
    public Table myTable;
    public ToggleManager myToggleManager;
    String selectivity_text;

    // Use this for initialization
    void Awake () {
        myTable = myChannel.getTable();
        if (myToggleManager.activeToggleid == 0) { selectivity_text = "Haría falta indicar si existe o no selectividad en frecuencia"; }
        else if (myToggleManager.activeToggleid ==1 ) { selectivity_text = "No existe selectividad en frecuencia"; }
        else  {
            selectivity_text = "Sí existe selectividad en frecuencia";
        }


        myReplyText.text = "[Enviar respuesta]: Hay que ajustar el analizador para centrarlo en "+ System.Math.Round(myTable.Getfc(),1)+
            " kHz, abarcando un ancho de banda de "+ System.Math.Round(myTable.GetSpan(), 2) + " kHz y amplitud de "+ myTable.GetAmplitude() + " dB. " + "\n\r" + selectivity_text;


    }
	
	// Update is called once per frame
	void Update () {
        myTable = myChannel.getTable();
        if (myToggleManager.activeToggleid == 0) { selectivity_text = "Haría falta indicar si existe o no selectividad en frecuencia"; }
        else if (myToggleManager.activeToggleid == 1) { selectivity_text = "No existe selectividad en frecuencia"; }
        else
        {
            selectivity_text = "Sí existe selectividad en frecuencia";
        }


        myReplyText.text = "[Enviar respuesta]: Hay que ajustar el analizador para centrarlo en " + System.Math.Round(myTable.Getfc(), 1) +
            " kHz, abarcando un ancho de banda de " + System.Math.Round(myTable.GetSpan(),2) + " kHz y amplitud de " + myTable.GetAmplitude() + " dB. " + "\n\r" + selectivity_text;

    }
}

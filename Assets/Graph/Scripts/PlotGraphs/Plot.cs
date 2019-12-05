using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using CodeMonkey.Utils;

/*
 * Plot: Incluye un objeto de tipo Graph,  
 * y métodos para obtener y actualizar los Tables
 * Los valores son extraídos de un fichero de texto
  */


public class Plot : MonoBehaviour {
    #region Fields
    public Graph glib;
    public RectTransform container;
    [SerializeField] public string url;
    public List<float> listaFloats = new List<float>();

    #endregion

    #region Properties
    public RectTransform getGraphContainer()  {
        return container;
    }

    public string getUrlFichero() {
        return url;
    }
    #endregion

    #region Methods

    // Use this for initialization

    protected virtual void Awake() {
        container = transform.Find("graphContainer").GetComponent<RectTransform>();
        glib = new Graph();
        glib.Initialize(this);
    }
    #endregion

    public IEnumerator GetText(String url) {

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)  {
            Debug.Log(www.error);
        }
        else  {
                        // Show results as text
            String texto = www.downloadHandler.text;
            Debug.Log("Contenido del archivo: " + texto);
            // Creamos una lista de strings en la que guardamos las divisiones de texto tomando como divisor los espacios del mismo
            List<string> ListaStrings = texto.Split(' ').ToList();


            // Rellenamos la lista de enteros covirtiendo cada valor de la lista de strings en un valor tipo int
            foreach (String s in ListaStrings) {
                if (s.Length > 0) listaFloats.Add(int.Parse(s.Trim()));
            }

            for (int i = 0; i < listaFloats.Count(); i++) {
                Debug.Log("Elemento lista " + i + ":" + listaFloats[i]);
            }

          }


    }
}

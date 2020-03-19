using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

/*
 * TableLib: Librería TableAdventure para representar una colección de puntos * 
 * conjunto de puntos en coma flotante, con valores entre 0 y 1
  */


[Serializable] public class Table {

    #region Fields
    [SerializeField] public string url;
    Boolean firstTime = true;
    List<float> lastResult = new List<float>(); //Valores recogidos del servidor
    private List<float> listToPlot = new List<float>();
    // Create a new SortedDictionary of floats.
    SortedDictionary<int, float> mysorteddictionary = new SortedDictionary <int, float>();
    #endregion

    #region Methods

    public IEnumerator RequestData(Plot plot, int x0, int x1) {
        if (firstTime == true)        {
            Debug.Log("Primera vez. Pido datos al servidor");
            return GetText(url, plot,x0, x1);
                    }
        plotGraphFromInterval (plot, x0, x1);
       return null;
    }

    public IEnumerator GetText(String url, Plot plot, int x0, int x1) {

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else  {
            // Show results as text
            String texto = www.downloadHandler.text;
            //Debug.Log("Contenido del archivo: " + texto);
            // Creamos una lista de strings en la que guardamos las divisiones de texto tomando como divisor los espacios del mismo
            List<string> ListaStrings = texto.Split(' ').ToList();

            // Rellenamos la lista de enteros covirtiendo cada valor de la lista de strings en un valor tipo int
            foreach (String s in ListaStrings)
            {
                if (s.Length > 0) lastResult.Add(float.Parse(s.Trim()));
            }

            for (int i = 0; i < lastResult.Count(); i++) {
                Debug.Log("Elemento lista " + i + ":" + lastResult[i]);
                mysorteddictionary.Add(i,lastResult [i]);
            }
            // plot.ShowGraph(lastResult);
            firstTime = false;
          }

        // Código redundante para que se ejecute
        for (int i = x0; i < x1 + 1; i++) {
            // SortedDictionary<int,float>.ValueCollection valueColl = mysorteddictionary.Values;
            listToPlot.Add(mysorteddictionary[i]); //Esta línea de código da fallos a veces i=0
        }

        plot.ShowGraph(listToPlot);
        int n = listToPlot.Count();
        // return n
    }


    public void plotGraphFromInterval(Plot plot, int x0, int x1)    {
        //Rellenamos listToPlot con los valores a representar entre x0 y x1
        for (int i =x0; i < x1+1; i++) {
            // SortedDictionary<int,float>.ValueCollection valueColl = mysorteddictionary.Values;
             listToPlot.Add(mysorteddictionary[i]);
            }

           plot.ShowGraph(listToPlot);
           int n= listToPlot.Count();  
        // return n
        }

       

    #endregion

}



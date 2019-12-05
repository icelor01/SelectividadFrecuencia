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


[Serializable]
public class Table {
    [SerializeField] public string url;    
    List<float> lastResult = new List<float>();

    public IEnumerator RequestData(Plot plot) {
        return GetText(url, plot);
    }

    public IEnumerator GetText(String url, Plot plot)
    {

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            String texto = www.downloadHandler.text;
            Debug.Log("Contenido del archivo: " + texto);
            // Creamos una lista de strings en la que guardamos las divisiones de texto tomando como divisor los espacios del mismo
            List<string> ListaStrings = texto.Split(' ').ToList();

            // Rellenamos la lista de enteros covirtiendo cada valor de la lista de strings en un valor tipo int
            foreach (String s in ListaStrings)
            {
                if (s.Length > 0) lastResult.Add(int.Parse(s.Trim()));
            }

            for (int i = 0; i < lastResult.Count(); i++)
            {
                Debug.Log("Elemento lista " + i + ":" + lastResult[i]);
            }
            plot.ShowGraph(lastResult);
        }
    }

  }



using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

/*
 * TableLib: Librería TableAdventure para representar una colección de puntos * 
 * conjunto de puntos en coma flotante, con valores entre 0 y 1
  */


[Serializable]
public class Table
{
    private static System.Random random = new System.Random();

    #region Fields
    [SerializeField] public string url;
    Boolean firstTime = true;

    // ultimo resultado para url
    List<float> lastResult = new List<float>();

    // cache de resultados
    SortedDictionary<int, float> sorted = new SortedDictionary<int, float>();
    #endregion

    #region Methods
    public void RequestData(Plot plot, int x0, int x1)
    {
        if (firstTime == true)
        {
            firstTime = false;
            Debug.Log("Primera vez. Pido datos al servidor para " + url, plot);
            plot.StartCoroutine(RequestFromServer(url, plot, x0, x1));
        }
        else
        {
            Debug.Log("Otra vez. Reutilizo datos viejos para " + url, plot);
            PlotGraphFromInterval(plot, x0, x1);
        }
    }

    public IEnumerator RequestFromServer(String url, Plot plot, int x0, int x1)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Cache-Control", "max-age=0, no-cache, no-store");
        www.SetRequestHeader("Pragma", "no-cache");
        www.useHttpContinue = false;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string result = www.downloadHandler.text;

            Debug.Log("=> " + url + " =>\n'" + result + "'", plot);
            string[] parts = result.Split(
                new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int i = 0;
            foreach (string s in parts) {
                try
                {
                    float f = float.Parse(s.Trim());
                    sorted.Add(i++, f);
                }
                catch {
                    Debug.Log("=> Invalid float string: '" + s.Trim() + "'", plot);
                }
            }
            Debug.Log("=> n = " + sorted.Count());

            firstTime = false;
        }
        PlotGraphFromInterval(plot, x0, x1);
        yield return null;
    }


    public void PlotGraphFromInterval(Plot plot, int x0, int x1)
    {
        //Rellenamos listToPlot con los valores a representar entre x0 y x1

        lastResult.Clear();
        StringBuilder pretty = new StringBuilder();

        for (int i = x0; i < x1 + 1; i++)
        {
            try
            {
                lastResult.Add(sorted[i]); 
                pretty.Append(sorted[i] + " ");
            }
            catch
            {
                Debug.Log(url + " no me da valores para i=" + i, plot);
            }
        }
        Debug.Log("=> pintando n = " + sorted.Count() + ":\n" + pretty, plot);

        plot.ShowGraph(lastResult);
    }

    #endregion
}



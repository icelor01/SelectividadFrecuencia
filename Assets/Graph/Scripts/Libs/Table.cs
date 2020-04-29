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
    [SerializeField] public string url_inicial;
    private String url;
    private string url_function;
    private int xmin;
    private int xmax;
    private int n;

    // Boolean firstTime = true; // Queremos que siempre pida los datos al servidor

    // ultimo resultado para url
    List<float> lastResult = new List<float>();

    // cache de resultados
    SortedDictionary<int, float> sorted = new SortedDictionary<int, float>();
    #endregion

    public void Initialize() {

        char[] delimiterChars = { '?', '=', '&' };
        string[] url_args = url_inicial.Split(delimiterChars);
        url_function = url_args[0];
        xmin = int.Parse(url_args[2]);
        xmax = int.Parse(url_args[4]);
        n = int.Parse(url_args[6]);
        url = url_inicial;

    }

    public void changeUrl (int xmin, int xmax, int n) {
        url = url_function + "?xmin="+xmin.ToString() + "&xmax=" + xmax.ToString() + "&n="+n.ToString();
    } 

    #region Methods
    public void RequestData(Plot plot)
        {
        /*
      if (firstTime == true)
      {
          firstTime = false;
          Debug.Log("Primera vez. Pido datos al servidor para " + url, plot);
          //plot.StartCoroutine(RequestFromServer(url, plot, x0, x1));
          plot.StartCoroutine(RequestFromServer(url, plot));
      }
      else
      {
          Debug.Log("Otra vez. Reutilizo datos viejos para " + url, plot);
          //PlotGraphFromInterval(plot, x0, x1);
          PlotGraphFromInterval(plot);
      }
      */
        Debug.Log("Pido datos al servidor para " + url, plot);

       

        //plot.StartCoroutine(RequestFromServer(plot, xmin, xmax, n));
        plot.StartCoroutine(RequestFromServer(url, plot));


    }

    //public IEnumerator RequestFromServer(Plot plot, int xmin, int xmax, int n)
    public IEnumerator RequestFromServer(String url, Plot plot)
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

                    float f = Single.Parse(s.Trim());
                    //float f = float.Parse(s.Trim());
                    sorted.Add(i++, f);
                                        
                }
                catch {
                    Debug.Log("Valor de s: " + s + " valor de single s: " + Single.Parse(s));
                    Debug.Log("=> Invalid float string: '" + s.Trim() + "'", plot);
                }
            }
            Debug.Log("=> n = " + sorted.Count());

            //firstTime = false;
        }
        //PlotGraphFromInterval(plot, x0, x1);
        PlotGraph(plot);
        yield return null;
    }

    public void PlotGraph(Plot plot)
    {
        lastResult.Clear();
        StringBuilder pretty = new StringBuilder();

        // for (int i = x0; i < x1 + 1; i++)
        for (int i = 0; i<sorted.Count(); i++)
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


    public string Truncate(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    #endregion
}



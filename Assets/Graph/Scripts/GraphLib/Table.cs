using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

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
    private float fc;
    private float span;
    private GameObject Graphname;
    private int xmin_solution;
    private int xmax_solution;
    private float fc_solution;
    private float amplitude;
    private float amplitude_solution;

    private int waitingForInputToStabilize = 0;
    bool askDataForFirstTime = true;

    // Boolean firstTime = true; // Queremos que siempre pida los datos al servidor

    // ultimo resultado para url
    List<float> lastResult = new List<float>();

    // cache de resultados
    SortedDictionary<int, float> sorted = new SortedDictionary<int, float>();
    #endregion

    #region Properties

    public void Setxmin(int xmin)
    {
        this.xmin = xmin;
    }

    public void Setxmax(int xmax)
    {
        this.xmax = xmax;
    }

    public void Setn(int n)
    {
        this.n = n;
    }

    public void Setfc(float fc)
    {
        this.fc = fc;
    }

    public void SetSpan(float span)
    {
        this.span = span;
    }

    public void SetAmplitude(float amplitude)
    {
        this.amplitude = amplitude;
    }

    public void SetAmplitudeSolution(float amplitude_solution)
    {
        this.amplitude_solution = amplitude_solution;
    }

    public int Getxmin()
    {
        return xmin;
    }

    public int Getxmax()
    {
        return xmax;
    }

    public int Getn()
    {
        return n;
    }

    public float GetAmplitude()
    {
        return amplitude;
    }

    public float Getfc()
    {
        return fc;
    }

    public float GetSpan()
    {
        return span;
    }

    public int Getxmin_solution()
    {
        return xmin_solution;
    }

    public int Getxmax_solution()
    {
        return xmax_solution;
    }

    public float GetAmplitude_solution()
    {
        return amplitude_solution;
    }
    #endregion
    #region Methods

    public void Initialize() {

        //Graphname = GameObject.Find("Window_graph_Channel");
        // if (Graphname != null)

        char[] delimiterChars = { '?', '=', '&' };
        string[] url_args = url_inicial.Split(delimiterChars);
        string url_function_total = url_args[0];
        string[] url_function_args = url_args[0].Split('/');
        url_function= url_function_args[4];

        if (url_function == "sincwave") 
        {                    
            xmin_solution = 1;
            xmax_solution = 16;
            amplitude_solution = 2;
            xmin = -50;
            xmax = 50;
            span = xmax - xmin;
            n = 100;
            amplitude = 1;
            url = GameManager.octaveServerEndpoint + url_function + "?xmin=" + xmin.ToString() + "&xmax=" + xmax.ToString() + "&n=" + n.ToString() + "&a=" + amplitude.ToString();
            //url = url_function + "?xmin=" + xmin.ToString() + "&xmax=" + xmax.ToString() + "&n=" + n.ToString() + "&amplitude=" + amplitude.ToString();
            Debug.Log("La función url es: " + url_function + "Estoy en el if");
            Debug.Log("La url es: " + url);
        }
        else 
        { 
        
            xmin = int.Parse(url_args[2]);
            xmax = int.Parse(url_args[4]);
            n = int.Parse(url_args[6]);
            amplitude= int.Parse(url_args[8]);
            
            // garantiza que la url empieza por GameManager.octaveServerEndpoint
            int ultimaBarra = url_inicial.LastIndexOf('/');
            url = GameManager.octaveServerEndpoint + url_inicial.Substring(ultimaBarra+1);
            Debug.Log("La función url es: " + url_function +"Estoy en el else");
            Debug.Log("La url es: " + url);
        }

    }

    public void Changefc(float fc)  {
        //Debug.Log("Valor inicial xmin:" + Getxmin());
        //Debug.Log("Valor inicial xmin:" + Getxmax());
        float valormediorango = ((xmax - xmin +1) / 2);
        int xmin_fc = (int) (fc - valormediorango);
        int xmax_fc = (int) (fc + valormediorango);
        //Debug.Log("Valor final xmin:" + xmin_fc);
        //Debug.Log("Valor final xmax:" + xmax_fc);
        Setfc(fc);
        ChangeUrl(xmin_fc, xmax_fc, Getn(), (int) GetAmplitude());
     }

    public void ChangeSpan(float span)
    {

        float half_span = (span+1)/ 2;
        int xmin_new = (int) (fc - half_span);
        int xmax_new = (int)(fc + half_span);
        SetSpan(span);
        ChangeUrl(xmin_new, xmax_new, Getn(), (int) GetAmplitude());
    }

    public void ChangeAmplitude(int amp_new)
    {
        SetAmplitude(amp_new);
        url = GameManager.octaveServerEndpoint + url_function + "?xmin=" + xmin.ToString() + "&xmax=" + xmax.ToString() + "&n=" + n.ToString() + "&a=" + amplitude.ToString();
        
    }

    public void ChangeUrl (int xmin_new, int xmax_new, int n, int amp_new) {
        Setxmin(xmin_new);
        Setxmax(xmax_new);
        Setn(n);

        url = GameManager.octaveServerEndpoint + url_function + "?xmin="+xmin.ToString() + "&xmax=" + xmax.ToString() + "&n="+n.ToString() + "&a=" + amplitude.ToString();
        //url = url_function + "?xmin=" + xmin.ToString() + "&xmax=" + xmax.ToString() + "&n=" + n.ToString() + "&amplitude=" + amplitude.ToString();
        Debug.Log("La nueva url es: "+url);
    }

    
    public IEnumerator DoTheDance(Plot plot) {
        waitingForInputToStabilize ++;
        Debug.Log(waitingForInputToStabilize);
        yield return new WaitForSeconds(1f); 
        waitingForInputToStabilize --; // will make the update method pick up 
        Debug.Log(waitingForInputToStabilize);
        if (waitingForInputToStabilize == 0) {
            RealRequestData(plot);
        }
    }
    public void RequestData(Plot plot) {
        Debug.Log("would now update plot", plot);
        plot.StartCoroutine(DoTheDance(plot));
    }
  
    public void RealRequestData(Plot plot)
    {

        //Video enabled
        if(!askDataForFirstTime) { 
        plot.Canvas_VideoPlayer.SetActive(true);
        }
        Debug.Log("Pido datos al servidor para " + url, plot);

        plot.StartCoroutine(RequestFromServer(url, plot));

    }

    //public IEnumerator RequestFromServer(Plot plot, int xmin, int xmax, int n)
    public IEnumerator RequestFromServer(String url, Plot plot)
    {
        sorted.Clear();
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
                float f = float.NaN;
                try
                {
                    f = Single.Parse(s.Trim(), CultureInfo.InvariantCulture);                    
                    sorted.Add(i++, f);              
                }
                catch {
                    Debug.Log("Valor de s: " + s + " valor de single s: " + f);
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

        // Video disabled
        plot.Canvas_VideoPlayer.SetActive(false);
        askDataForFirstTime = false;

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



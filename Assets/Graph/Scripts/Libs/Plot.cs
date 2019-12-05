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

    [SerializeField] private Sprite circleSprite;
    [SerializeField] public string url;
    public float yMaximum;
    public float yMin;
    public int totalValores;
    public Table table;
    public RectTransform container;
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



   public void Initialize(Plot plot) {
        this.plot = plot;
        plot.StartCoroutine(plot.GetText(plot.getUrlFichero()));
        this.ShowGraph(plot.listaFloats);
   }

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

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(plot.getGraphContainer(), false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<float> listaFloats)  {
        float graphHeight = plot.getGraphContainer().sizeDelta.y;
        float yMaximum = 100f; //Amplitud ó máximo valor de la gráfica
        float graphWidth = plot.getGraphContainer().sizeDelta.x;
        totalValores = listaFloats.Count();
        //Debug.Log("Número de valores total de la lista:" + totalValores);
        float stepWidth = graphWidth / totalValores;
        float xSize = stepWidth;
        //float xSize = 50f; //Paso de 50 unidades. Habría que definir según número de puntos a representar

       
        foreach (GameObject o in last)  {
           GameObject.Destroy(o);
        }
        last.Clear();
       

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < listaFloats.Count; i++)  {
            float xPosition = xSize + i * xSize;
            float yPosition = (listaFloats[i] / yMaximum) * graphHeight;
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            last.Add(circleGameObject);
            if (lastCircleGameObject != null)
            {
                last.Add(CreateDotConnection(
                    lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition)
                );
            }
            lastCircleGameObject = circleGameObject;
        }
    }


    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)  {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(plot.getGraphContainer(), false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
        return gameObject;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using CodeMonkey.Utils;

/*
 * Graph: Dibuja una gráfica a partir de una lista o tabla? de valores tipo Float
 * Dada una tabla, pintar una parte o con un nivel de zoom dado
 */

public class Graph {

    [SerializeField] private Sprite circleSprite;
    public float yMaximum;
    public float yMin;
    public int totalValores;
    public List<GameObject> last;
    public Plot plot;
   
    public void Initialize(Plot plot) {
        this.plot = plot;
        last = new List<GameObject>();
        plot.StartCoroutine(plot.GetText(plot.getUrlFichero()));
        this.ShowGraph(plot.listaFloats);
    }

    // Dada una lista, tenemos que llamar a método ShowGraph para que pinte


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


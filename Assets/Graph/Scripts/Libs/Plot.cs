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


public class Plot : MonoBehaviour
{
    #region Fields

    [SerializeField] public Table table;
    [SerializeField] private Sprite circleSprite;
    private float yMaximum;
    private float yMin;
    private int totalValores;
    private RectTransform container;
    private List<float> listaFloats = new List<float>();
    private List<GameObject> last = new List<GameObject>();

    #endregion

    #region Properties

    public Table getTable() {
        return table;
    }
    public void setTable(Table table) {
        this.table = table;
    }

    public RectTransform getGraphContainer()
    {
        return container;
    }
    #endregion

    #region Methods

    public void Initialize()
    {
        StartCoroutine(table.RequestData(this));
    }

    protected virtual void Awake()
    {
        container = transform.Find("graphContainer").GetComponent<RectTransform>();
        Initialize();
    }
    #endregion

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(getGraphContainer(), false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    public void ShowGraph(List<float> listaFloats)
    {
        this.listaFloats = listaFloats;
        float graphHeight = getGraphContainer().sizeDelta.y;
        float yMaximum = 100f; //Amplitud ó máximo valor de la gráfica
        float graphWidth = getGraphContainer().sizeDelta.x;
        totalValores = listaFloats.Count();
        Debug.Log("Número de valores total de la lista:" + totalValores);
        float stepWidth = graphWidth / totalValores;
        float xSize = stepWidth;
        //float xSize = 50f; //Paso de 50 unidades. Habría que definir según número de puntos a representar


        foreach (GameObject o in last)
        {
            GameObject.Destroy(o);
        }
        last.Clear();


        GameObject lastCircleGameObject = null;
        for (int i = 0; i < listaFloats.Count; i++)
        {
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


    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(getGraphContainer(), false);
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

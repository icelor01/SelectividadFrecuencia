using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using CodeMonkey.Utils;

/*
 * Plot: Incluye un objeto de tipo Table y métodos para obtener y actualizar Tables
 * Los valores son extraídos de un fichero de texto
  */


public class Plot : MonoBehaviour
{
    #region Fields

    [SerializeField] public Table table;
    [SerializeField] private Sprite circleSprite;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    //private RectTransform dashTemplateX;
    //private RectTransform dashTemplateY;
    //[SerializeField] private float yMin;
    //[SerializeField] private float yMax;
    private int totalValores;
    private RectTransform container;
    private List<float> listaFloats = new List<float>();
    private List<GameObject> last = new List<GameObject>();
    public Plot plot;

    #endregion

    #region Properties

    public Table getTable()
    {
        return table;
    }
    public void setTable(Table table)
    {
        this.table = table;
    }

    public RectTransform getGraphContainer()
    {
        return container;
    }
    #endregion

    #region Methods
    protected virtual void Awake()
    {
        container = transform.Find("graphContainer").GetComponent<RectTransform>();
        labelTemplateX = container.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = container.Find("labelTemplateY").GetComponent<RectTransform>();
        //dashTemplateX = container.Find("dashTemplateX").GetComponent<RectTransform>();
        //dashTemplateY = container.Find("dashTemplateY").GetComponent<RectTransform>();
        Initialize();
    }

    public void Initialize()
    {
        plot = this;

        Refresh();
    }

    public void Refresh()
    {
        table.Initialize();
        table.RequestData(plot);
            }


    public void ShowGraph(List<float> listaFloats, Func<float, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {

        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (float _i) { return _i.ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        }

        this.listaFloats = listaFloats;
        float graphHeight = getGraphContainer().sizeDelta.y;
        //float yMaximum = 100f; //Amplitud ó máximo valor de la gráfica -> En vez de asignar valor fijo, lo hacemos variable
        float graphWidth = getGraphContainer().sizeDelta.x;
        totalValores = listaFloats.Count();
        float stepWidth = graphWidth / totalValores;
        float xSize = stepWidth;
        //float xSize = 50f; //Paso de 50 unidades. Habría que definir según número de puntos a representar


        foreach (GameObject o in last)
        {
            GameObject.Destroy(o);
        }
        last.Clear();

        
        float yMaximum = listaFloats[0];
        float yMinimum = listaFloats[0];

        foreach (int value in listaFloats)
        {
            if (value > yMaximum)
            {
                yMaximum = value;
            }
            if (value < yMinimum)
            {
                yMinimum = value;
            }
        }

        yMaximum = yMaximum + ((yMaximum - yMinimum) * 0.2f);
        yMinimum = yMinimum - ((yMaximum - yMinimum) * 0.2f);

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < listaFloats.Count; i++)
        {
            //float xPosition = i * xSize;
            //float yPosition = (listaFloats[i] / yMax) * graphHeight;
            float xPosition = xSize + i * xSize;
            float yPosition = ((listaFloats[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            
            if (yMinimum < 0)
            {
                yPosition = graphHeight / 2 + (listaFloats[i] / yMaximum) * graphHeight / 2; //Normalizamos el valor de y
            }
            
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition)); //create circle
            last.Add(circleGameObject);
            if (lastCircleGameObject != null)
            {
                last.Add(CreateDotConnection(
                    lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                    circleGameObject.GetComponent<RectTransform>().anchoredPosition)); //create connection between dots
            }
            lastCircleGameObject = circleGameObject;

            int separatorCountX = 10;
            for (int i_X = 0; i_X <= separatorCountX; i_X++)
            {
                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(container, false);
                labelX.gameObject.SetActive(true);
                float normalizedValue = i_X * 1f / separatorCountX; // valor normalizado entre 0 y 1
                //labelX.anchoredPosition = new Vector2(xPosition, -7f);
                labelX.anchoredPosition = new Vector2(normalizedValue * graphWidth, -7f);
                //labelX.GetComponent<Text>().text = i.ToString();
                //labelX.GetComponent<Text>().text = getAxisLabelX(i);
                int xmin = table.getxmin();
                int xmax = table.getxmax();
                labelX.GetComponent<Text>().text = getAxisLabelX((xmin + (normalizedValue * (xmax - xmin))));
                last.Add(labelX.gameObject);
            }
            /*
            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(container, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, -7f);
            //gameObjectList.Add(dashX.gameObject);
            */
        }

        int separatorCountY = 10;
        for (int i_Y = 0; i_Y <= separatorCountY; i_Y++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(container, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i_Y * 1f / separatorCountY; // valor normalizado entre 0 y 1
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            //labelY.GetComponent<Text>().text = (normalizedValue * yMaximum).ToString();
            labelY.GetComponent<Text>().text = getAxisLabelY(yMinimum + (normalizedValue * (yMaximum - yMinimum)));
            last.Add(labelY.gameObject);

            /*
            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(container, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            //gameObjectList.Add(dashY.gameObject);
           */ 
        }
    }

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

    #endregion
}

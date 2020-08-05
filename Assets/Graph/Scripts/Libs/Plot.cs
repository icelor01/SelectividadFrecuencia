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
    private Plot plot;

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

    private GameObject lastCircleGameObject = null;

    // ranges are constant
    // return normalized value of X
    
    private float RangeToScreenX (float value, float domainMin, float domainMax, float screenMin, float screenMax)
    {
        float domainRange = (domainMax - domainMin)-1;
        float screenRange = screenMax - screenMin;
        float normalizedValue = (value - domainMin) / domainRange; // value now in range 0 to 1
       return (normalizedValue * screenRange) + screenMin; // value now in range screenMin to screenMax
    }

    // ranges are constant
    // return normalized value of Y with margins

    private float RangeToScreenY (float value, float domainMin, float domainMax, float screenMin, float screenMax) {
        float domainRange = domainMax - domainMin;
        float screenRange = screenMax - screenMin;
        float normalizedValue = (value - domainMin) / domainRange; // value now in range 0 to 1
        float normalizedValue_withMargins = normalizedValue * .8f + .1f; // between .1 and .9, both included
        return (normalizedValue_withMargins * screenRange) + screenMin; // value now in range screenMin to screenMax
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
        lastCircleGameObject = null;
        if (getAxisLabelX == null)
        {
            getAxisLabelX = delegate (float _i) { return System.Math.Round(_i,1).ToString(); };
        }
        if (getAxisLabelY == null)
        {
            getAxisLabelY = delegate (float _f) { return System.Math.Round(_f, 1).ToString(); };
        }

        this.listaFloats = listaFloats;
        float graphHeight = getGraphContainer().rect.height;
        //float yMaximum = 100f; //Amplitud ó máximo valor de la gráfica -> En vez de asignar valor fijo, lo hacemos variable
        float graphWidth = getGraphContainer().rect.width;
        totalValores = listaFloats.Count();
        float xSize = graphWidth / (totalValores-1);
        //float xSize = 50f; //Paso de 50 unidades. Habría que definir según número de puntos a representar
        //Debug.Log("Ancho: " + graphWidth + "; Alto: " + graphHeight, this);


        foreach (GameObject o in last)
        {
            GameObject.Destroy(o);
        }
        last.Clear();

        
        float yMaximum = listaFloats[0];
        float yMinimum = listaFloats[0];

        foreach (float value in listaFloats)
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

        for (int i = 0; i < totalValores; i++)
        {
            float xPosition = RangeToScreenX(i, 0, totalValores, 0, graphWidth);
            float yPosition = RangeToScreenY(listaFloats[i], yMinimum, yMaximum, 0, graphHeight);            
            AddPoint(xPosition, yPosition);

            /*
            //Label X
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(container, false);
            labelX.gameObject.SetActive(true);
            float normalizedValueX = i * 1f / (totalValores - 1); // valor normalizado entre 0 y 1
                                                                      
            labelX.anchoredPosition = new Vector2(normalizedValueX * graphWidth, -7f);
            int xmin = table.getxmin();
            int xmax = table.getxmax();
            labelX.GetComponent<Text>().text = getAxisLabelX((xmin + (normalizedValueX * (xmax - xmin))));
            last.Add(labelX.gameObject);
            */

        }

        //Label X
            
        int xmin = table.Getxmin();
        int xmax = table.Getxmax();
        
        int valores_ejeX_max = 10;
        int separatorCountX;

        if (totalValores <= valores_ejeX_max)   {
            separatorCountX = totalValores;
          
            for (int i = 0; i < totalValores; i++) {
                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(container, false);
                labelX.gameObject.SetActive(true);
                float normalizedValueX = i * 1f / (totalValores - 1); // valor normalizado entre 0 y 1
                labelX.anchoredPosition = new Vector2(normalizedValueX * graphWidth, -7f);
                labelX.GetComponent<Text>().text = getAxisLabelX((xmin + (normalizedValueX * (xmax - xmin))));
                last.Add(labelX.gameObject);
            }

        }
        else {

            separatorCountX = valores_ejeX_max; 


                for (int i_X = 0; i_X <separatorCountX; i_X++) {
                        RectTransform labelX = Instantiate(labelTemplateX);
                        labelX.SetParent(container, false);
                        labelX.gameObject.SetActive(true);
                        float normalizedValue = i_X * 1f / (separatorCountX - 1); // valor normalizado entre 0 y 1
                                                                     //labelX.anchoredPosition = new Vector2(xPosition, -7f);
                    labelX.anchoredPosition = new Vector2(normalizedValue * graphWidth, -7f);
                    labelX.GetComponent<Text>().text = getAxisLabelX((xmin + (normalizedValue * (xmax - xmin))));
                    last.Add(labelX.gameObject);
                }
        }
        
        /*
        RectTransform dashX = Instantiate(dashTemplateX);
        dashX.SetParent(container, false);
        dashX.gameObject.SetActive(true);
        dashX.anchoredPosition = new Vector2(xPosition, -7f);
        //gameObjectList.Add(dashX.gameObject);
        */
        //int separatorCountY = 10;
        
        //Label Y // haria falta pintar escala entre yMinimum e yMaximun con n valores ordenados
        int separatorCountY=10;

        for (int i_Y = 0; i_Y < separatorCountY; i_Y++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(container, false);
            labelY.gameObject.SetActive(true);
            float normalizedPosY = (i_Y * 1f / (separatorCountY- 1))*0.8f + 0.1f; // valor normalizado entre 0 y 1
            labelY.anchoredPosition = new Vector2(-7f, normalizedPosY * graphHeight + yMinimum);
            float normalizedValueY = (i_Y * 1f / (separatorCountY - 1)); // valor normalizado entre 0 y 1
            // Escala
            labelY.GetComponent<Text>().text = getAxisLabelY(yMinimum + normalizedValueY * (yMaximum - yMinimum));
            //labelY.GetComponent<Text>().text = (normalizedValue * yMaximum).ToString();
            //labelY.GetComponent<Text>().text = getAxisLabelY(listaFloats[i_Y]);
            //last.Add(labelY.gameObject);
            Debug.Log("Valor " + i_Y + ":" + labelY.GetComponent<Text>().text);

            
            /*
            //Dash Y
            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(container, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            //gameObjectList.Add(dashY.gameObject);
           */

        }

    }


    private void AddPoint(float x, float y)
    {
        GameObject circleGameObject = CreateCircle(new Vector2(x, y));
        last.Add(circleGameObject);
        if (lastCircleGameObject != null)
        {
            last.Add(CreateDotConnection(
                lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition,
                circleGameObject.GetComponent<RectTransform>().anchoredPosition)); //create connection between dots
        }
        lastCircleGameObject = circleGameObject;
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

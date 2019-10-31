using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using CodeMonkey.Utils;

public class Plot : MonoBehaviour {
    #region Fields
    public GraphLib glib;
    public RectTransform container;
    //public string url;
    [SerializeField] public string url;
    #endregion

    #region Properties
    public RectTransform getGraphContainer() {
         return container;
    }

    public string getUrlFichero()  {
        return url;
    }
    #endregion

    #region Methods

    // Use this for initialization

    protected virtual void Awake () {
        container = transform.Find("graphContainer").GetComponent<RectTransform>();
        glib = new GraphLib();
        glib.Initialize(this);
     }
    #endregion
}

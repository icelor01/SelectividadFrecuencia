using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPosition : MonoBehaviour
    {

    public int posX;
    public int posY;
    public int posZ;

    RectTransform myRectTransform;

    private void Awake()
    {
        myRectTransform = GetComponent<RectTransform>();
        Vector3 position = new Vector3(posX, posY, posZ);
        //Debug.Log("Mi posición inicial es: " + myRectTransform.position);
        myRectTransform.localPosition = position;
        //Debug.Log("Mi posición es: " + myRectTransform.position);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

   
    // Update is called once per frame
    void Update()
    {
      

    }
}

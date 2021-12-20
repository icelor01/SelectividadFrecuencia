using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckFrecuency : MonoBehaviour {

    public Plot myChannel;
    public Table myTable;
    [SerializeField] private float delayBeforeLoading = 100f;
    private float timeElapsed;


    // Use this for initialization
    void Awake () {
        myTable = myChannel.getTable();
    }
	
	// Update is called once per frame
	void Update () {
        myTable = myChannel.getTable();
        if (System.Math.Round(myTable.Getfc(), 0) == 10) {
            //StartCoroutine(Waiter());
            Delay();
            GameManager.manager.GoToScene("Tutorial2");
        }
        else
        {
            // Debemos mantenernos en la misma escena
            //do nothing
        }
    }

    private void Delay()
    {
        timeElapsed = 0;
        while (timeElapsed < delayBeforeLoading)
        {
            //Wait
            timeElapsed += Time.deltaTime;
        }

    }


    IEnumerator Waiter()
    {
        //Wait for 10 seconds
        yield return new WaitForSeconds(10);


    }
}

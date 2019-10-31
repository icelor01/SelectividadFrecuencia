using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameObjectsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("GameObject.Start"); //prints a String into our console
        // new GameObject(); creates in Hierarchy a new GameObject
        //new GameObject("MyNewGameObject"); //creates a new GameObject with name
        GameObject myGameObject = new GameObject("MyNewGameObject", typeof(SpriteRenderer));

    }

    // Update is called once per frame
    void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;


[System.Serializable]
public class Exercise
{
    public int id;
    public string type;
    public string url;
    public string musica;
    public string fileName;

    // Start is called before the first frame update
    void Awake()
    {

    }

    public void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }


    public static Exercise Load(String fileName)
    {

        // Deserialización
        StreamReader reader = new StreamReader(Application.dataPath + "/" + fileName);
        string jsonString = reader.ReadToEnd();
        Debug.Log("JSON loaded string is: " + jsonString);
        Exercise myExercise = JsonUtility.FromJson<Exercise>(jsonString);
        return myExercise;
        //Debug.Log("JSON id is: " + myExercise.id);
    }

    private void JsonTextReader(object sr)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        // Serialización

        /*
        Exercise myExercise = new Exercise();
        myExercise.id = 1;
        myExercise.url = "http://localhost:5000/f/new_process/iname_theta?xmin=0&xmax=10&numValues=30&numGraphs=2&a=1&b=2";
        myExercise.type = "media_y_correlacion";
        myExercise.musica = "titulo_cancion.mp3";
        */

        //string json = JsonUtility.ToJson(myExercise);
        string json = JsonUtility.ToJson(this);
        Debug.Log("JSON saved string is: " + json);

    }


}

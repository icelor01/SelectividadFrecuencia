using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class ExerciseList
{
    public List<Exercise> Exercises;
    // Particularizar para tutoriales o niveles
    public string type;

    public static ExerciseList FromJson(string listJsonFile)
    {
        // Deserialización
        StreamReader reader = new StreamReader(Application.dataPath + "/" + listJsonFile);
        string jsonListString = reader.ReadToEnd();
        Debug.Log("JSON exercises string is: " + jsonListString);
        Debug.Log("JSON exercise List loaded is: " + JsonUtility.FromJson<ExerciseList>(jsonListString));
        ExerciseList exerciseList = JsonUtility.FromJson<ExerciseList>(jsonListString);
        Debug.Log("JSON exercise List loaded is: " + exerciseList.ToJson());
        return exerciseList;
    }


    public string ToJson()
    {
        return JsonUtility.ToJson(this);

    }

}

// Code extracted from http://javiginer.com/unity-variables-persistentes-entre-escenas/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class ProjectVars : Singleton<ProjectVars> {
    /// <summary>
    /// Este string estara activo entre escenas
    /// </summary>
    public int score;
 
    public static ProjectVars Instance
    {
        get
        {
            return ((ProjectVars)mInstance);
        }
        set
        {
            mInstance = value;
        }
    }
 
    protected ProjectVars() { }
}

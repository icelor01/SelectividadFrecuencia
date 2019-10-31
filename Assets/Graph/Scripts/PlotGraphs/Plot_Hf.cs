using UnityEngine;

public class Plot_Hf : Plot  {
    override protected void Awake()  {
        url = "http://localhost:5000/x?a=sinewave.m&b=10";
        base.Awake();
    }
}

using UnityEngine;

public class Plot_Yf : Plot  {
    override protected void Awake()  {
        url = "http://localhost:5000/x?a=z.m&b=20";
        base.Awake();
    }
}

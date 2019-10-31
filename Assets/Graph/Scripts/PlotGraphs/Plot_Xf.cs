using UnityEngine;

public class Plot_Xf : Plot {
    override protected void Awake() {
        url = "http://localhost:5000/x?a=square_signal.m&b=10&c=100";
        base.Awake();
    }	
}

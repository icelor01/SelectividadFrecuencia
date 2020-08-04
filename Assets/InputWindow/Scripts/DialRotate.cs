using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialRotate : MonoBehaviour {

    public List<float> rotationStop = new List<float>();
    public float sensitivity;
    public float dialValue = 0;

    // Update is called once per frame
    void Update()
    {

        // Set the maximun stop position to the total number of stops
        dialValue = Mathf.Clamp(dialValue, 0, rotationStop.Count - 1);

        // Determine which stop is selected based on knob value from 0-1
        if (dialValue >= 0 && dialValue < 1)
            TurnDialTo(rotationStop[0]);
        if (dialValue >= 1 && dialValue < 2)
            TurnDialTo(rotationStop[1]);
        if (dialValue >= 2 && dialValue < 3)
            TurnDialTo(rotationStop[2]);
        if (dialValue >= 3 && dialValue <= 4)
            TurnDialTo(rotationStop[3]);
        if (dialValue >= 4 && dialValue <= 5)
            TurnDialTo(rotationStop[4]);
    }

    // Dragging on the gameobject increases or decreases dialvalue
    void OnMouseDrag()
    {
        float dragMove = Input.GetAxis("Mouse X") * sensitivity;
        dialValue += dragMove;
    }

    // Rotates the knob to angle set in rotationStop
    void TurnDialTo(float position)
    {
        transform.rotation = Quaternion.Euler(0, position, 0);
    }
}

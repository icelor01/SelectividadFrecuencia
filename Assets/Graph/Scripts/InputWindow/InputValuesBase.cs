using UnityEngine;
using UnityEngine.UI;

public abstract class InputValuesBase : MonoBehaviour
{
    public InputField InputFreq;
    [SerializeField] public GameObject plotComponent;
    // Each slider must have a min, max and an initital value
    [SerializeField] protected int min_value;
    [SerializeField] protected int max_value;
    [SerializeField] protected float initial_value;
    float fill_ini = 1f; //FillAmount value from 0 to 1

    void Start()
    {
        Plot plot = plotComponent.GetComponent<Plot>();
        Table table = plot.getTable();
    }

    public abstract void UpdatePlot(Plot plot, Table table, float value);
}

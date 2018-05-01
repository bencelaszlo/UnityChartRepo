// system
using System.Collections.Generic;
using System.Linq;
// unity
using UnityEngine;

namespace UnityChart
{
namespace Base
{

public class ChartHolder : MonoBehaviour
{
    public float[] X;
    public List<float[]> YSet = new List<float[]>(); // multiple lines support
    public string[] Unit;

    public Color LayoutColor;
    public Color PlaneColor;
    public Color GridColor;
    public Color LineColor;

    public float LineWidth;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        var customChartObjects = GameObject.FindGameObjectsWithTag("ChartLayout");
        foreach (GameObject go in customChartObjects)
        {
            if (go.GetComponent<LineRenderer>() != null)
            {
                go.GetComponent<LineRenderer>().startColor = LayoutColor;
                go.GetComponent<LineRenderer>().endColor = LayoutColor;
            }
            else if (go.GetComponent<TextMesh>() != null)
                go.GetComponent<TextMesh>().color = LayoutColor;
        }

        customChartObjects = GameObject.FindGameObjectsWithTag("ChartGrid");
        foreach (GameObject go in customChartObjects)
        {
            if (go.GetComponent<LineRenderer>() != null)
            {
                go.GetComponent<LineRenderer>().startColor = GridColor;
                go.GetComponent<LineRenderer>().endColor = GridColor;
            }
        }

        customChartObjects = GameObject.FindGameObjectsWithTag("ChartLine");
        foreach (GameObject go in customChartObjects)
        {
            if (go.GetComponent<LineRenderer>() != null)
            {
                go.GetComponent<LineRenderer>().startColor = LineColor;
                go.GetComponent<LineRenderer>().endColor = LineColor;
                go.GetComponent<LineRenderer>().startWidth = 1;
                go.GetComponent<LineRenderer>().endWidth = 1;
                go.GetComponent<LineRenderer>().widthMultiplier = LineWidth;
            }
        }

        GameObject.Find("Plane").GetComponent<MeshRenderer>().material.color = PlaneColor;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateChart()
    {
        // Destroy chart line container
        DestroyImmediate(this.gameObject.transform.Find("Lines").gameObject);

        // Create new chart line container
        GameObject lineContainer = new GameObject("Lines");
        lineContainer.transform.parent = this.gameObject.transform;

        // Get text game object containing tick text meshes
        Transform textGroup = this.gameObject.transform.Find("Layout").Find("Text");

        // Set default chart bounds on tick text meshes
        textGroup.Find("XFirstValue").GetComponent<TextMesh>().text = "";
        textGroup.Find("XLastValue").GetComponent<TextMesh>().text = "";
        textGroup.Find("YFirstValue").GetComponent<TextMesh>().text = "";
        textGroup.Find("YLastValue").GetComponent<TextMesh>().text = "";

        // Set default chart units
        textGroup.Find("XUnit").GetComponent<TextMesh>().text = "";
        textGroup.Find("YUnit").GetComponent<TextMesh>().text = "";

        if (X.Length == 0 || YSet.Count == 0)
        {
            return;
        }
        
        // Get minimum and maximum values in x array
        float minX = X.Min();
        float maxX = X.Max();

        // Define chart bounds in x axis
        float[] xBounds = new float[] { minX, maxX };

        // Get minimum and maximum values in y set
        float minY = YSet.Select(p => p.Min()).Min();
        float maxY = YSet.Select(p => p.Max()).Max();

        // Define chart bounds in y axis
        float[] yBounds = new float[]{
        minY - Mathf.Abs(minY * 0.1f), // min minus 10%
        maxY + Mathf.Abs(maxY * 0.1f) }; // max plus 10%

        // Create chart lines
        ChartObjectFactory factory = FindObjectOfType<ChartObjectFactory>();
        foreach (float[] Y in YSet)
        {
            // Instantiate chart line from prefab
            ChartLine line = factory.InstantiateChartLine(
                this.gameObject.transform.Find("Lines"), "Line").GetComponent<ChartLine>();
            // Set x and y points
            line.X = X;
            line.Y = Y;
            // Set chart bounds
            line.XBounds = xBounds;
            line.YBounds = yBounds;
            // Generate line
            line.UpdateLine();
        }

        // Set chart bounds on tick text meshes
        textGroup.Find("XFirstValue").GetComponent<TextMesh>().text = xBounds[0].ToString("0.0#");
        textGroup.Find("XLastValue").GetComponent<TextMesh>().text = xBounds[1].ToString("0.0#");
        textGroup.Find("YFirstValue").GetComponent<TextMesh>().text = yBounds[0].ToString("0.0#");
        textGroup.Find("YLastValue").GetComponent<TextMesh>().text = yBounds[1].ToString("0.0#");

        // Set chart units
        textGroup.Find("XUnit").GetComponent<TextMesh>().text = Unit[0];
        textGroup.Find("YUnit").GetComponent<TextMesh>().text = Unit[1];

        // Update interaction component
        if (this.gameObject.GetComponent<InteractableChartBase>())
        {
            this.gameObject.GetComponent<InteractableChartBase>().UpdateInteraction();
        }
    }
}

}
}

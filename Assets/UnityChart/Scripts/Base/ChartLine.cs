// unity
using UnityEngine;

namespace UnityChart
{
namespace Base
{

public class ChartLine : MonoBehaviour
{
    public float[] X;
    public float[] Y;

    public float[] XBounds { set; get; }
    public float[] YBounds { set; get; }

    private float[] origin = { 0.0f, 0.0f };
    private float[] size = { 150.0f, 50.0f };

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateLine()
    {
        // Compute X and Y axis projections
        var multX = (size[0] - origin[0]) / (XBounds[1] - XBounds[0]);
        var multY = (size[1] - origin[1]) / (YBounds[1] - YBounds[0]);
        float[] projX = new float[X.Length];
        float[] projY = new float[Y.Length];
        for (int i = 0; i < X.Length; ++i)
        {
            projX[i] = multX * (X[i] - XBounds[0]);
            projY[i] = multY * (Y[i] - YBounds[0]);
        }

        // Fill line positions
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = X.Length;
        for (int i = 0; i < X.Length; ++i)
        {
            lineRenderer.SetPosition(i, new Vector3(projX[i], projY[i], 0.0f));
        }
    }
}

}
}

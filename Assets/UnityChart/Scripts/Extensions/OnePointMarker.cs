// unity
using UnityEngine;
// local
using UnityChart.Base;

namespace UnityChart
{
namespace Extensions
{

public class OnePointMarker : MarkerBase
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void UpdateMarker(int index)
    {
        base.UpdateMarker(index);

        // Get chart line component
        ChartLine chartLine = FindObjectOfType<ChartLine>();

        // Get line renderer component
        LineRenderer lineRenderer = chartLine.GetComponent<LineRenderer>();

        // Get marker point position
        Vector3 markerPosition = lineRenderer.GetPosition(index);

        // Set marker position
        this.gameObject.transform.localPosition = new Vector3(
            markerPosition.x, markerPosition.y, this.gameObject.transform.localPosition.z);

        // Define marker line component
        this.gameObject.transform.Find("Line").GetComponent<LineRenderer>().SetPosition(0, new Vector3(0, -markerPosition.y - 2.2f));

        // Define marker text
        this.gameObject.transform.GetComponentInChildren<TextMesh>().text = chartLine.GetComponent<ChartLine>().Y[index].ToString("0.00#");
    }
}

}
}

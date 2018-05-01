// unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
// UnityChart
using UnityChart.Base;
using UnityChart.Extensions;

public class SinCosChartWithMarkerTest : MonoBehaviour
{
    private float[] xData;
    private float[] sinData;
    private float[] cosData;

    private bool displaySinus = true;
    private bool displayCosinus = false;

    private GameObject chart;

    private Slider slider;

    private UnityAction<float> sliderChangedAction;

    // Use this to initialize any variables or game state before the game starts
    private void Awake()
    {
        sliderChangedAction = new UnityAction<float>(OnSliderValueChanged);
    }

    // Use this for initialization
    void Start ()
    {
        // Define x and y test data
        int numOfSamples = 50;
        float spacing = 0.5f;
        xData = new float[numOfSamples];
        sinData = new float[numOfSamples];
        cosData = new float[numOfSamples];
        for (int i = 0; i < numOfSamples; ++i)
        {
            float x = i * spacing;
            xData[i] = x;
            sinData[i] = Mathf.Sin(x);
            cosData[i] = Mathf.Cos(x);
        }

        // Chart factory
        ChartObjectFactory chartHolderFactory = FindObjectOfType<ChartObjectFactory>();

        // Instantiate chart game object
        chart = chartHolderFactory.InstantiateChartHolder(this.gameObject.transform, "ChartHolder");

        // Add Interactable component to the chart
        MarkerInteractableChart interactableComponent = chart.AddComponent<MarkerInteractableChart>();
        interactableComponent.MarkerIndexList.Add(0); // marker at position X: 0
        interactableComponent.MarkerColorList.Add(Color.red); // marker color: red

        UpdateChartDsiplay();

        // Configure slider
        slider = GameObject.FindObjectOfType<Slider>();
        slider.minValue = 0;
        slider.maxValue = numOfSamples - 1;
        slider.value = 0;
        slider.onValueChanged.AddListener(sliderChangedAction);
    }

	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnSliderValueChanged(float value)
    {
        if (!chart)
        {
            return;
        }

        // Modify marker position
        chart.GetComponent<MarkerInteractableChart>().ModifyMarkerPointIndex((int)value);
    }

    public void SinusToggleValueChanged(Toggle toggle)
    {
        displaySinus = toggle.isOn;
        UpdateChartDsiplay();
    }

    public void CosinusToggleValueChanged(Toggle toggle)
    {
        displayCosinus = toggle.isOn;
        UpdateChartDsiplay();
    }

    private void UpdateChartDsiplay()
    {
        // Set chart data
        ChartHolder chartHolder = chart.GetComponent<ChartHolder>();
        chartHolder.X = xData;
        chartHolder.YSet.Clear();
        string yLabel = "";
        if (displaySinus)
        {
            chartHolder.YSet.Add(sinData);
            yLabel = "Sin(A)";
        }
        if (displayCosinus)
        {
            chartHolder.YSet.Add(cosData);
            if (displaySinus)
            {
                yLabel += " & ";
            }
            yLabel += "Cos(A)";
        }
        chartHolder.Unit = new string[] { "X: A", "Y: " + yLabel };

        // Update chart
        chartHolder.UpdateChart();
    }
}

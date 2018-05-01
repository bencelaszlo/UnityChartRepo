# UnityChartRepo
Unity project for world and screen space rendering simple 2D charts in Unity. It allows the creation of customizable and interactive 2D line charts.

You can try a live demo here (built with Unity WebGL).

All coding contributions and comments are welcome.

## Usage

* Clone/download the repository
* Copy the entire *UnityChart* folder into your Unity project *Assets* folder
* Attach the *Assets/UnityChart/Scripts/Base/ChartObjectFactory.cs* and *Assets/UnityChart/Scripts/Extensions/ExtensionsObjectFactory.cs* to a unique empty game object.

Get chart factory component instance:

 ```cs
 ChartObjectFactory chartHolderFactory = FindObjectOfType<ChartObjectFactory>();
 ```

 Instantiate a new chart holder game object:

 ```cs
 chart = chartHolderFactory.InstantiateChartHolder(parent, name);
 ```

 Setting chart data and calling to update:
 
 ```cs
 // Set chart data
 ChartHolder chartHolder = chart.GetComponent<ChartHolder>();
 chartHolder.X = x_data;
 chartHolder.YSet.Add(y_data); // add more data to display multiple lines in the same chart
 chartHolder.Unit = new string[] { "[x_unit]", "[y_unit]" };
 // Update chart
 chartHolder.UpdateChart();
 ```
 
## Notes

The chart layout object is created in the 3D scene as a combination of 3D objects (plane, lines and text). There is a camera and a directional light pointing to it.
To render the chart in screen space as an UI object, you should render the chart as a texture using the camera in front and designate this texture as the texture material of a *RawImage* UI object. As an example, you can find a default render texture *ChartCameraTexture* in *UnityChart/Materials*. By default, all instances of the chart are being rendered to this texture. Create new render textures if you'd like to render multiple charts at the same time.

The charts can be made interactive by using other UI objects, which can be set to modify the chart element properties and values. The class *InteractableChartBase.cs* (inherited from *MonoBehaviour*) can be inherited and assigned to the chart holder game object to extend the chart interactivity, append new elements to the chart layout, etc. Any inherited class and assigned to the chart holder game object will automatically update after every call to the chart holder update.

 Add an interactable component to the chart (if necessary):
 
 ```cs
 MarkerInteractableChart interactableComponent = chart.AddComponent<MarkerInteractableChart>(); // inherits from InteractableChartBase.cs
 interactableComponent.MarkerIndexList.Add(0); // marker at position X: 0
 interactableComponent.MarkerColorList.Add(Color.red); // marker color: red
 ```

The *UnityChart/Scripts/Extensions/MarkerInteractableChart.cs* contains a set of tools to include graph markers along the x-axis. This markers can be made movable by using a complementary UI element (e.g. a slider) which controls the position of the marker in the x-axis:

 ```cs
 private void OnUISliderValueChanged(float value)
 {
     // Modify marker position
     chart.GetComponent<MarkerInteractableChart>().ModifyMarkerPointIndex((int)value);
 }
 ```

For the live demo a customized and hidden slider is manually positioned and fitted above the chart layout. Then, the user is able to interact with the slider as if it was interacting directly with the chart marker.

All chart element game objects are set to lie in layer `20: UnityChart`. You can make them invisible to any camera by using the camera's culling mask option.
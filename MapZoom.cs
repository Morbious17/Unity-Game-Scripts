using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapZoom : MonoBehaviour
{
    //This script controls the slider next to the map images. Scrolling it up and down allows the player to 
    //zoom in and out of the map. The player icon also gets smaller and larger at the same rate
    //as the map so that it appears it's being zoomed in as well. 
    //The if statements keep the map from being moved out of view.

    [SerializeField] private Slider MapZoomSlider;
    [SerializeField] private GameObject MapContent;
    [SerializeField] private RawImage TestMap1;
    [SerializeField] private RawImage TestMap2;
    [SerializeField] private GameObject PlayerPanel;

    private float SliderValue;

	void Start ()
    {
		
	}

	void Update ()
    {
        //SliderValue = MapZoomSlider.value;
        //Debug.Log(MapZoomSlider.value);
        //Debug.Log(TestMap1.transform.localScale);
        //Debug.Log(TestMap1.transform.localPosition);

        TestMap1.transform.localScale = new Vector3(MapZoomSlider.value, MapZoomSlider.value);

        TestMap2.transform.localScale = new Vector3(MapZoomSlider.value, MapZoomSlider.value);

        MapContent.transform.localScale = new Vector3(MapZoomSlider.value / 2, MapZoomSlider.value / 2);

        PlayerPanel.transform.localScale = new Vector3(MapZoomSlider.value, MapZoomSlider.value);

        if (MapContent.transform.localScale.x < 0.5f)
        {
            MapContent.transform.localScale = new Vector3(0.5f, 0.5f);
        }
        else
        {
            MapContent.transform.localScale = TestMap1.transform.localScale;
        }

        if (MapContent.transform.localPosition.x < -75)
        {
            MapContent.transform.localPosition = new Vector3(-75, MapContent.transform.localPosition.y);
        }

        if (MapContent.transform.localPosition.x > 150)
        {
            MapContent.transform.localPosition = new Vector3(150, MapContent.transform.localPosition.y);
        }

        if (MapContent.transform.localPosition.y < -87)
        {
            MapContent.transform.localPosition = new Vector3(MapContent.transform.localPosition.x, -87);
        }

        if (MapContent.transform.localPosition.y > 33)
        {
            MapContent.transform.localPosition = new Vector3(MapContent.transform.localPosition.x, 33);
        }        
	}  
}

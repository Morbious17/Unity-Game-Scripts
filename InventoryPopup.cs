using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button survivalButton;
    [SerializeField] private Button collectibleButton;
    [SerializeField] private Button mapButton;
    [SerializeField] private Button optionButton;

    //public ColorBlock buttonColor;
    //public Color normalColor;

	void Start ()
    {
        //buttonColor = inventoryButton.colors;
        //normalColor = new Color(255, 200, 0, 255);
	}

	void Update ()
    {
		
	}

    public void ButtonClicked()
    {

    }
}

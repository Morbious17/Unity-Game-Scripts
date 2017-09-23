using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImages : MonoBehaviour
{
    //This script assigns the appropriate image to an item panel when an item is collected and
    //removes it when there are no more items.
    [SerializeField] private Sprite SmallFirstAidKit;
    [SerializeField] private Sprite LargeFirstAidKit;
    [SerializeField] private Sprite PistolAmmo;
    [SerializeField] private Sprite ShotgunAmmo;
    [SerializeField] private Sprite RifleAmmo;
    [SerializeField] private Sprite Fuel;

    private Image imageComponent;

	void Start ()
    {
       imageComponent = GetComponent<Image>();
	}

	void Update ()
    {
		
	}

    //TODO: Set image to white when adding image.
    public void SetSmallKit()
    {
        imageComponent.color = new Color(1, 1, 1, 1);
        imageComponent.sprite = SmallFirstAidKit;
    }

    public void SetLargeKit()
    {
        imageComponent.color = new Color(1, 1, 1, 1);
        imageComponent.sprite = LargeFirstAidKit;
    }

    public void SetPistolAmmo()
    {
        imageComponent.color = new Color(1, 1, 1, 1);
        imageComponent.sprite = PistolAmmo;
    }

    public void SetShotgunAmmo()
    {
        imageComponent.color = new Color(1, 1, 1, 1);
        imageComponent.sprite = ShotgunAmmo;
    }

    public void SetRifleAmmo()
    {
        imageComponent.color = new Color(1, 1, 1, 1);
        imageComponent.sprite = RifleAmmo;
    }

    public void SetFuel()
    {
        imageComponent.color = new Color(1, 1, 1, 1);
        imageComponent.sprite = Fuel;
    }

    //TODO: Set color to black when removing image.
    public void RemoveImage()
    {
        imageComponent.color = new Color(0, 0, 0, 1);
        imageComponent.sprite = null;
    }
}

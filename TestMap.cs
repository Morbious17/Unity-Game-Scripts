using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMap : MonoBehaviour
{
    [SerializeField] private MapManager mapManager = null;

	void Start ()
    {
		
	}


    void Update ()
    {
		
	}

    private void HitByRay()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            mapManager.RevealMap();
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
  [SerializeField] private HUDManager hudManager = null;
    private float damage = 80;

	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void HitByRay()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            hudManager.TakeDamage(damage);
        }
    }
}

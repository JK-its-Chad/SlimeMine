using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {



	void Start ()
    {
		
	}
	
	void Update ()
    {

	}

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Weapon")
        {
            other.GetComponent<SlimeEnemy>().health -= 10;
        }
        
    }
}

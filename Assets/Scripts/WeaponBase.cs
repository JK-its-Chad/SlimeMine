using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {

<<<<<<< HEAD
	// Use this for initialization
	void Start () {

=======


	void Start ()
    {
		
>>>>>>> 0d9cb09654085c413c11fb3d9fea9172d915f93d
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

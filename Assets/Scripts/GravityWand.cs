using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWand : MonoBehaviour {

    public bool grabbed = false;
    GameObject[] enemies;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(grabbed)
        {
            enemies = GameObject.FindGameObjectsWithTag("Corpse");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position).normalized * -100);
            }
        }
	}
}

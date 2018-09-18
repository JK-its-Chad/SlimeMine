using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public int health = 100;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Input.GetAxis("Oculus_GearVR_LThumbstickX") * Time.deltaTime, 0, Input.GetAxis("Oculus_GearVR_LThumbstickY") * Time.deltaTime);
	}
}

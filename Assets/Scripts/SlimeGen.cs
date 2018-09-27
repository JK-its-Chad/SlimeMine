using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGen : MonoBehaviour {

    public Transform aim;
    public GameObject[] slime;
    public bool spawn = true;
	
	void Update ()
    {
		if(Input.GetButton("Fire1") && spawn)
        {
            RaycastHit info;
            if (Physics.Raycast(aim.position, Vector3.forward, out info, 10))
            {
                Instantiate(slime[0], info.transform.position, Quaternion.Euler(Vector3.forward));
            }
        }
	}
}

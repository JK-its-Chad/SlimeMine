using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public OVRInput.Controller controller;
    public string buttonName;
    GameObject grabbedObject;
    bool grabbing;
    public float grabRadius = .13f;
    public LayerMask grabMask;

    Quaternion lastRotation;
    Quaternion currentRotation;

    void GrabObject()
    {
        grabbing = true;

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0f, grabMask);

        if(hits.Length > 0)
        {
            int closestHit = 0;
            for(int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance > hits[closestHit].distance) closestHit = i;
                Debug.Log(hits[i].ToString());
            }
            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform;
            grabbedObject.tag = "Weapon";
        }

    }
    void DropObject()
    {
        grabbing = false;

        if(grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();

            grabbedObject = null;
            grabbedObject.tag = "Object";
        }
    }

    Vector3 GetAngularVelocity()
    {
        Quaternion deltaRotation = currentRotation * Quaternion.Inverse(lastRotation);
        return new Vector3(Mathf.DeltaAngle(0, deltaRotation.eulerAngles.x),
                           Mathf.DeltaAngle(0, deltaRotation.eulerAngles.y),
                           Mathf.DeltaAngle(0, deltaRotation.eulerAngles.z));
    }

	void Update ()
    {
        if(grabbedObject !=null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }

        Debug.Log(Input.GetAxis(buttonName));
		if(!grabbing && Input.GetAxis(buttonName) >= 0.9 ) GrabObject();
        if(grabbing && Input.GetAxis(buttonName) < 0.9) DropObject();
    }
}

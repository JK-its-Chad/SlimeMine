using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour {

    public Movement player;
    public OVRInput.Controller controller;
    public string buttonName;
    GameObject grabbedObject;
    bool grabbing;
    public float grabRadius = .13f;
    public LayerMask grabMask;

    Quaternion lastRotation;
    Quaternion currentRotation;
    int roaz = 0;

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
            }
            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            roaz = Mathf.RoundToInt(grabbedObject.transform.eulerAngles.x / 90);
            Debug.Log(roaz);
            grabbedObject.transform.parent = transform;
            if(grabbedObject.GetComponent<WeaponBase>())
            {
                grabbedObject.transform.position = transform.position;
                WeaponBase weapon = grabbedObject.GetComponent<WeaponBase>();
                weapon.grabbed = true;
                weapon.controller = controller;
                weapon.player = player;
                weapon.transform.localRotation = Quaternion.Euler(roaz * 90, weapon.snapY, weapon.snapZ);//x: 0sword, 90key, 180knife, 270seppuku
            }
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

            if (grabbedObject.GetComponent<WeaponBase>())
            {
                grabbedObject.GetComponent<WeaponBase>().grabbed = false;
                grabbedObject.GetComponent<WeaponBase>().controller = OVRInput.Controller.None;
                grabbedObject.GetComponent<WeaponBase>().player = null;
            }

            grabbedObject = null;
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

		if(!grabbing && Input.GetAxis(buttonName) >= 0.9 ) GrabObject();
        if(grabbing && Input.GetAxis(buttonName) < 0.9) DropObject();
    }
}

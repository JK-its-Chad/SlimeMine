using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

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

        if (hits.Length > 0)
        {
            int closestHit = 0;
            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].distance > hits[closestHit].distance) closestHit = i;
            }
            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            //roaz = Mathf.RoundToInt(grabbedObject.transform.eulerAngles.x / 90);
            //float angleX = grabbedObject.transform.eulerAngles.x;

            Vector3 paz = ConvertQuant2Euler(grabbedObject.transform.rotation);

            float angleX = paz.x;

            if (angleX < 45 || angleX > 315)
            {
                roaz = 0;
            }
            else if (angleX >= 45 && angleX <= 135)
            {
                roaz = 1;
            }
            else if (angleX > 135 && angleX < 225)
            {
                roaz = 2;
            }
            else if (angleX >= 225 && angleX <= 315)
            {
                roaz = 3;
            }
            grabbedObject.transform.parent = transform;
            if (grabbedObject.GetComponent<GravityWand>())
            {
                grabbedObject.GetComponent<GravityWand>().grabbed = true;
            }
            if (grabbedObject.GetComponent<WeaponBase>())
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

        if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(controller);
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = GetAngularVelocity();

            if (grabbedObject.GetComponent<GravityWand>())
            {
                grabbedObject.GetComponent<GravityWand>().grabbed = false;
            }
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

    void Update()
    {
        if (grabbedObject != null)
        {
            lastRotation = currentRotation;
            currentRotation = grabbedObject.transform.rotation;
        }

        if (!grabbing && Input.GetAxis(buttonName) >= 0.9) GrabObject();
        if (grabbing && Input.GetAxis(buttonName) < 0.9) DropObject();
    }

    public static Vector3 ConvertQuant2Euler(Quaternion quaternion)
    {
        float tempEuler;
        float[] eulerAngles = new float[3];

        //Convert pitch - X
        tempEuler = Mathf.Atan2(2 * quaternion.x * quaternion.w + 2 * quaternion.y * quaternion.z, 1 - 2 * quaternion.x * quaternion.x - 2 * quaternion.z * quaternion.z);
        eulerAngles[0] = tempEuler * 180 / Mathf.PI;

        //Convert yaw - Y
        tempEuler = Mathf.Asin(2 * quaternion.x * quaternion.y + 2 * quaternion.z * quaternion.w);
        eulerAngles[1] = tempEuler * 180 / Mathf.PI;

        //Convert roll - Z
        tempEuler = Mathf.Atan2(2 * quaternion.y * quaternion.w + 2 * quaternion.x * quaternion.z, 1 - 2 * quaternion.y * quaternion.y - 2 * quaternion.z * quaternion.z);
        eulerAngles[2] = tempEuler * 180 / Mathf.PI;

        return new Vector3(eulerAngles[0], eulerAngles[1], eulerAngles[2]);
    }
}

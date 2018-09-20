using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    public int health = 100;
    public int slime = 0;
    public Image hitSplat;
    public Text healthText;
    public GameObject camera;

    private float timer = 0;
	
	void Update ()
    {

        timer -= Time.deltaTime;
        Vector3 MoveDir = new Vector3(Input.GetAxis("Oculus_GearVR_LThumbstickX"), 0, Input.GetAxis("Oculus_GearVR_LThumbstickY")) * Time.deltaTime;
        Vector3 cameraRot = camera.transform.rotation.eulerAngles;
        transform.position += Quaternion.Euler(0, cameraRot.y, 0) * MoveDir;
        if(timer >= 0)
        {
            hitSplat.color = new Color(Color.red.r, Color.red.g, Color.red.b, (Color.red.a * timer) * 0.4f);
        }
        else hitSplat.color = Color.clear;
        if (timer >= -2)
        {
            healthText.color = new Color(Color.green.r, Color.green.g, Color.green.b, (Color.green.a * (timer + 2)));
        }
        else healthText.color = Color.clear;
    }

    public void takeDamage(int damage)
    {
        if (timer <= 0)
        {
            health -= damage;
            timer = 1;
            healthText.text = "Health: " + health;
            if (health <= 0) die();
        }
    }

    private void die()
    {

    }
}

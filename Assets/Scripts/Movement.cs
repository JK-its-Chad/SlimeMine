﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour {

    public int health = 100;
    public int slime = 0;
    public Image hitSplat;
    public Text healthText;
    public Text GameOver;
    public Text SlimeCollected1;
    public TextMesh SlimeCollected2;
    public Text GameTimer;
    public Text otherText;
    public GameObject camera, spawner;
    public Vector3 FromMe;
    float angle = 0;

    private float timer = 0;
    private float gameTimer = 0;
    public bool dead = false;
    private int minutes = 0, seconds = 0;

    private void Start()
    {
        SlimeCollected1.color = Color.clear;
        otherText.color = Color.clear;
        GameOver.color = Color.clear;
    }

    void Update ()
    {
        FromMe = gameObject.transform.position;
        timer -= Time.deltaTime;

        if (!dead)
        {
            if (seconds >= 60) minutes++;
            seconds = (int)Time.realtimeSinceStartup - (minutes * 60);
            if(seconds <= 10)
            {
                GameTimer.text = "Time: " + minutes + ":0" + seconds;
            }
            else GameTimer.text = "Time: " + minutes + ":" + seconds;

            Vector3 MoveDir = new Vector3(Input.GetAxis("Oculus_GearVR_LThumbstickX"), 0, Input.GetAxis("Oculus_GearVR_LThumbstickY")) * Time.deltaTime;
            Vector3 cameraRot = camera.transform.rotation.eulerAngles;
            if (Input.GetAxis("Oculus_GearVR_RThumbstickX") > .9 || Input.GetAxis("Oculus_GearVR_RThumbstickX") < -.9)
            {
                angle += Input.GetAxis("Oculus_GearVR_RThumbstickX");
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.y + angle * .5f , 0));
            }
            //camera.transform.rotation = Quaternion.Euler(cameraRot);
            transform.position += Quaternion.Euler(0, cameraRot.y, 0) * MoveDir;
            if (timer >= 0)
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
        SlimeCollected1.text = SlimeCollected2.text;
        SlimeCollected1.color = Color.green;
        otherText.color = Color.green;
        GameOver.color = Color.red;
        dead = true;
        spawner.GetComponent<Spawner>().spawn = false;
    }
}

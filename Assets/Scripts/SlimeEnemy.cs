using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour {

    public GameObject target;
    bool grounded = true;
    public GameObject tail;
    public int damage = 5;
    public Material deadRed;

    public int health = 100;
    public float jumpHeight = 100;
    public int movementSpeed = 10;
    public int lookSpeed = 2;
    public int slimeValue = 10;

    private float ITimer = 0.75f;
    public bool dead = false;

    Rigidbody rig;

	void Start ()
    {
        rig = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
    {

        if (!dead)
        {
            if (gameObject.GetComponent<Rigidbody>().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody>().velocity.y >= -0.01f)
            {
                grounded = true;
            }

            if (grounded)
            {
                Quaternion playerRotation = Quaternion.LookRotation(
                            (new Vector3(target.transform.position.x, 0, target.transform.position.z))
                            - (new Vector3(transform.position.x, 0, transform.position.z)));

                gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, lookSpeed);

                if (playerRotation == transform.rotation)
                {
                    rig.AddForce(transform.forward.x * movementSpeed, jumpHeight, transform.forward.z * movementSpeed);
                    grounded = false;
                }

            }
        }
        if (target.GetComponent<Movement>().dead == true)
        {
            movementSpeed = -20;
        }
        
    }

    private void Update() //for timer logic
    {
        ITimer -= Time.deltaTime;
    }

    public bool takeDamage(int damage, Movement player)
    {
        if (ITimer <= 0 && !dead)
        {
            health -= damage;
            ITimer = 0.75f;
            if (health <= 0) Die(player);
            return true;
        }

        return false;
    }

    private void Die(Movement player)
    {
        dead = true;
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.layer = 9;
            if (child.gameObject.GetComponent<MeshRenderer>())
            {
                child.gameObject.GetComponent<MeshRenderer>().material = deadRed;
            }
        }
        gameObject.layer = 9;
        rig.freezeRotation = false;
    }
}

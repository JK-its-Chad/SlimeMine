using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour {

    public GameObject target;
    bool grounded = true;
    public GameObject tail;

    public int health = 100;
    public float jumpHeight = 100;
    public int movementSpeed = 10;
    public int lookSpeed = 2;

    private float ITimer = 0.75f;

    Rigidbody rig;

	void Start ()
    {
        rig = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
    {

        if(gameObject.GetComponent<Rigidbody>().velocity.y <= 0.01f && gameObject.GetComponent<Rigidbody>().velocity.y >= -0.01f)
        {
            grounded = true;
        }
        
        if (grounded)
        {
            Quaternion playerRotation = Quaternion.LookRotation(
                        (new Vector3(target.transform.position.x, 0 , target.transform.position.z))
                        - (new Vector3(transform.position.x, 0, transform.position.z)));

            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, lookSpeed);

            if(playerRotation == transform.rotation) 
            {
                rig.AddForce(transform.forward.x * movementSpeed, jumpHeight, transform.forward.z * movementSpeed);
                grounded = false;
            }

        }
        
    }

    private void Update() //for timer logic
    {
        ITimer -= Time.deltaTime;
    }

    private void dealDmg()
    {
        if(Vector3.Distance(gameObject.transform.position, target.transform.position) > .5)
        {
            target.GetComponent<Movement>().health -= 5;
        }
    }

    public bool takeDamage(int damage)
    {
        if (ITimer <= 0)
        {
            health -= damage;
            ITimer = 0.75f;
            if (health <= 0) Die();
            return true;
        }

        return false;
    }

    private void Die()
    {
        movementSpeed = 0;
        lookSpeed = 0;
        gameObject.layer = 9;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour {

    public GameObject target;
    bool grounded = true;
    public GameObject tail;

    public int health = 100;
    int lookspeed = 2;
    int movementSpeed = 10;
    Rigidbody rig;

	void Start ()
    {
        rig = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
    {
		if(health <= 0)
        {
            Die();
        }
        
        if (grounded)
        {
            Quaternion lookAtMeSenpai = Quaternion.LookRotation(
                        (new Vector3(target.transform.position.x, 0 , target.transform.position.z))
                        - (new Vector3(transform.position.x, 0, transform.position.z)));

            gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtMeSenpai, lookspeed);

            if(lookAtMeSenpai == transform.rotation)
            {
                rig.AddForce(transform.forward.x * movementSpeed, 100, transform.forward.z * movementSpeed);
                grounded = false;
            }

        }
        
    }

    void dealDmg()
    {
        if(Vector3.Distance(gameObject.transform.position, target.transform.position) > .5)
        {
            target.GetComponent<Movement>().health -= 5;
        }
    }

    void Die()
    {
        movementSpeed = 0;
        lookspeed = 0;
        gameObject.layer = 9;
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }

}

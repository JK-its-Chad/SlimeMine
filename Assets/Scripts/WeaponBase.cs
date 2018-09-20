using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour {

    private Rigidbody rB;
    private List<GameObject> enemiesHit = new List<GameObject>();

    public LayerMask detectMask;
    public int damage = 10; 
    public int durability = 100;    // weapon durability
    public float damageSpeed;         // speed required for a swung object to deal damage
    public bool swingX = true;      
    public bool swingY = true;      // bools for if swinging an object in a local direction deals damage
    public bool swingZ = true;  
    public bool hasDurability;
    public bool grabbed = false;
    public OVRInput.Controller controller;
    public Movement player;

    protected List<GameObject> dealDamage()
    {
        if (grabbed)
        {
            foreach (GameObject enemy in enemiesHit)
            {
                if (enemy.GetComponent<SlimeEnemy>())
                {

                    if (enemy.GetComponent<SlimeEnemy>().takeDamage(damage, player) && hasDurability)
                    {
                        Debug.Log("enemy hit");
                        durability--;
                        if (durability <= 0) Destroy(gameObject);
                    }
                }
                else
                {
                    enemiesHit.Remove(enemy);
                }
            }
            return enemiesHit;
        }
        return null;
    }

    protected void Start()
    {
        rB = gameObject.AddComponent<Rigidbody>();
    }

    protected void Update()
    {
        if (controller != OVRInput.Controller.None)
        {
            Vector3 localVel = OVRInput.GetLocalControllerVelocity(controller);
            if (swingX)
            {
                if (localVel.x >= damageSpeed)
                {
                    dealDamage();
                }
                else if (localVel.x <= -damageSpeed)
                {
                    dealDamage();
                }
            }
            else if (swingY)
            {
                if (localVel.y >= damageSpeed)
                {
                    dealDamage();
                }
                else if (localVel.y <= -damageSpeed)
                {
                    dealDamage();
                }
            }
            else if (swingZ)
            {
                if (localVel.z >= damageSpeed)
                {
                    dealDamage();
                }
                else if (localVel.z <= -damageSpeed)
                {
                    dealDamage();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if ((detectMask == (detectMask | (1 << other.gameObject.layer))) && !enemiesHit.Contains(other.gameObject))
        {
            enemiesHit.Add(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (detectMask == (detectMask | (1 << other.gameObject.layer)))
        {
            if (enemiesHit.Contains(other.gameObject))
            {
                enemiesHit.Remove(other.gameObject);
            }
        }
    }

}

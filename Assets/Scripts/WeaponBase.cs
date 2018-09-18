using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour {

    public int damage;

    private void OnTriggerEnter(Collider other)
    { 
        if(gameObject.tag == "Weapon")
        {
            other.GetComponent<SlimeEnemy>().takeDamage(damage);
        }
        
    }
}

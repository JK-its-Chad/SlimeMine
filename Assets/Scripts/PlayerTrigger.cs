using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Movement player;
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == 10)
        {
            player.takeDamage(other.gameObject.GetComponentInParent<SlimeEnemy>().damage);
        }
    }

}

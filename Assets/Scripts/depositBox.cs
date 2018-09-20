using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class depositBox : MonoBehaviour {

    public TextMesh value;
    public int slime;
	void Update () {
        value.text = slime.ToString();
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponentInParent<SlimeEnemy>())
        {
            if (collision.gameObject.GetComponentInParent<SlimeEnemy>().dead)
            {
                slime += collision.gameObject.GetComponentInParent<SlimeEnemy>().slimeValue;
                Destroy(collision.gameObject);
            }
        }
    }
}

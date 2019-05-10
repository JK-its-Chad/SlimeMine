using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject slime;
    public GameObject player;
    private float randomNum1;
    private float randomNum2;
    private float timer = 3;
    public bool spawn = true;
    public float baseSpawnTime = 5;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0 && spawn)
        {
            GameObject newSlime = Instantiate(slime, RandomCircle(Vector3.zero, 20), Quaternion.identity);

            newSlime.GetComponent<SlimeEnemy>().target = player;

            newSlime.transform.localScale = Vector3.one * Random.Range(.4f, 1);

            timer = (baseSpawnTime - (Time.realtimeSinceStartup/100));            
        }
    }

    Vector3 RandomCircle(Vector3 center, float radius)
    {
        float ang = Random.value * 360;
        float angZ = Random.Range(-90f, 90f);
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = 50;
        pos.z = center.z + radius * Mathf.Cos(angZ * Mathf.Deg2Rad);
        return pos;
    }
}

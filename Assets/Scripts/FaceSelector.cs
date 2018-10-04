using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSelector : MonoBehaviour {

    public TextMesh text;

    List<string> face = new List<string> { ":3", ":D", ":P", ":X", ":O",":V", "=)", "=/", ":'(", ":c", ":o", ":b", ":s", "xD", "x3",
                                           "( ͡° ͜ʖ ͡°)", "OwO", "UwU", "oAo", ">o<", "-w-", "-v-", "OvO", "OmO", "OuO", "OnO", "o3o",
                                           "^w^", "^u^", ">w>", "-u-", "<w<", ">v>", "UvU", "uwu", 
                                           "u3u", "-3-", "=w=", "=3=", "=n=", "@w@",
                                           "0.0", "._.", "T.T", "^_^"};

    void Start ()
    {
        int choice = Random.Range(0, 45);
        if(choice >= 0 && choice <= 14)
        {
            text.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        if(choice >= 15 && choice <= 40)
        {
            text.characterSize = 30;
        }
        text.text = face[choice];
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(gameObject.GetComponent<SlimeEnemy>().dead)
        {
            text.characterSize = 50;
            text.text = "x.x";
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSelector : MonoBehaviour {

    public TextMesh text;

    List<string> face = new List<string> { ":3", ":D", ":P", ":X", ":O",":V", "( ͡° ͜ʖ ͡°)", "0.0", "._.", "OwO", "UwU", "T.T" };

    void Start ()
    {
        int choice = Random.Range(0, 8);
        if(choice >= 0 && choice <= 5)
        {
            text.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90));
        }
        if(choice == 6)
        {
            text.characterSize = 30;
        }
        text.text = face[choice];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

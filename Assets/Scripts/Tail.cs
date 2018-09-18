using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public GameObject Slime;
    public Vector3 lastPosition;
    public Vector3 nextPosition;

    private float _timer = 0.5f;


    void Update()
    {
        if (Slime == null) return;

        _timer = Mathf.Clamp(_timer + Time.deltaTime * 100, 0, 1);

        float distanceToNextPosition = Vector3.Distance(transform.position, nextPosition);

        if (distanceToNextPosition > 0)
        {
            Debug.Log("Lerp");
            transform.position = Vector3.Lerp(lastPosition, nextPosition, _timer);
        }

        lastPosition = nextPosition;
        nextPosition = ((Slime.transform.position + (Slime.transform.forward * -.3f) - (Slime.transform.up * 0.02f)));
    }
}
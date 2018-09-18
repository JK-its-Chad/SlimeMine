using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Tail : MonoBehaviour
    {
        public GameObject Slime;
        public Vector3 lastPosition;
        public Vector3 nextPosition;

        private float _timer = 0;


        void Update()
        {
            if (Slime == null) return; 

            _timer = Mathf.Clamp(_timer + Time.deltaTime, 0, 1); 

            float distanceToNextPosition = Vector3.Distance(transform.position, nextPosition);

            if (distanceToNextPosition > .2f) 
            {
                transform.position = Vector3.Lerp(lastPosition, nextPosition, _timer);
            }

            else 
            {
                lastPosition = nextPosition;
                nextPosition = Slime.transform.position;
                _timer = 0;
            }
        }
    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_movement : MonoBehaviour {

    public int player = 0;
    public float acceleration = 10f;


	// Use this for initialization
	void Start () {
		
	}
	
	void FixedUpdate () {
        if (Input.GetAxis("Accelerate") != 0)
        {
            Debug.Log("accelerating");
            GetComponent<Rigidbody>().AddForce(transform.forward * acceleration);
        }

        if (Input.GetButton("Break"))
        {
            Debug.Log("breaking");
        }
		
        if (Input.GetAxis("Horizontal") != 0)
        {
            Debug.Log("Horizontal Movement");
        }
		
	}
}

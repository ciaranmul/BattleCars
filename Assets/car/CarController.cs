﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    Rigidbody body;
    float deadZone = 0.1f;
    public float groundedDrag = 3f;
    public float maxVelocity = 50;
    public float hoverForce = 1000;
    public float gravityForce = 1000f;
    public float hoverHeight = 1.5f;
    public GameObject[] hoverPoints;

    public float forwardAcceleration = 8000f;
    public float reverseAcceleration = 4000f;
    float thrust = 0f;

    public float turnStrength = 1000f;
    float turnValue = 0f;

    public ParticleSystem[] dustTrails = new ParticleSystem[2];

    int layerMask;
    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody>();
        body.centerOfMass = Vector3.down;

        layerMask = 1 << LayerMask.NameToLayer("Vehicle");
        layerMask = ~layerMask;

        Debug.Log(string.Format("Player {0} found", this.GetComponent<CarGenerateInputStrings>().player));
	}
	
	// Update is called once per frame
	void Update () {
        // Main Thrust
        thrust = 0.0f;
        float acceleration = Input.GetAxis(this.GetComponent<CarGenerateInputStrings>().accelerate);
        if (acceleration > deadZone)
            thrust = acceleration * forwardAcceleration;
        else if (acceleration < -deadZone)
            thrust = acceleration * reverseAcceleration;

        // Turning
        turnValue = 0.0f;
        float turnAxis = Input.GetAxis(this.GetComponent<CarGenerateInputStrings>().horizontal);
        if (Mathf.Abs(turnAxis) > deadZone)
            turnValue = turnAxis;
	}

    // Update once per physics engine tick
    void FixedUpdate()
    {
        RaycastHit hit;
        bool grounded = false;
        for (int i = 0; i < hoverPoints.Length; i++)
        {
            var hoverPoint = hoverPoints[i];
            if (Physics.Raycast(hoverPoint.transform.position, -Vector3.up, out hit, hoverHeight, layerMask))
            {
                body.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
                grounded = true;
            }
            else
            {
                // Self-Levelling - returns the vehicle to horizontal when not grounded
                if (transform.position.y > hoverPoint.transform.position.y)
                {
                    body.AddForceAtPosition(hoverPoint.transform.up * gravityForce, hoverPoint.transform.position);
                }
                else
                {
                    body.AddForceAtPosition(hoverPoint.transform.up * -gravityForce, hoverPoint.transform.position);
                }
            }
        }

        var emissionRate = 0;
        if (grounded)
        {
            body.drag = groundedDrag;
            emissionRate = 10;
        }
        else
        {
            body.drag = 0.1f;
            thrust /= 100f;
            turnValue /= 100f;
        }

        for (int i = 0; i < dustTrails.Length; i++)
        {
            var emission = dustTrails[i].emission;
            emission.rateOverDistance = new ParticleSystem.MinMaxCurve(emissionRate);
        }

        // Handle forward and reverse forces
        if (Mathf.Abs(thrust) > 0)
            body.AddForce(transform.forward * thrust);

        // Handle turn forces
        if (turnValue > 0)
        {
            body.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
        }
        else if (turnValue < 0)
        {
            body.AddRelativeTorque(Vector3.up * turnValue * turnStrength);
        }

        // Limit max velocity
        if (body.velocity.sqrMagnitude > (body.velocity.normalized * maxVelocity).sqrMagnitude)
        {
            body.velocity = body.velocity.normalized * maxVelocity;
        }
    }

}

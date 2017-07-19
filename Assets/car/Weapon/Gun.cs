using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour {

    public float range = 1000;
    public LayerMask collisionMask;
    public float fireRate;

    public Transform spawn;
    private LineRenderer tracer;
    public GameObject hitParticle;

    private float secondsBetweenShots;
    private float nextPossibleShootTime;

	// Use this for initialization
	void Start () {
        secondsBetweenShots = 60 / fireRate;
        if (GetComponent<LineRenderer>())
        {
            tracer = GetComponent<LineRenderer>();
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void Shoot()
    {
        float shotDistance = range;

        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            // Play Gunshot
            GetComponent<AudioSource>().Play();

            // Set next time player can shoot
            nextPossibleShootTime = Time.time + secondsBetweenShots;

            // Draw tracer
            if (tracer)
            {
                Vector3[] parms = new Vector3[2] { ray.origin, ray.GetPoint(shotDistance) };
                StartCoroutine("RenderTracer", parms);
            }

            // Detect hit
            if (Physics.Raycast(ray, out hit, shotDistance, collisionMask))
            {
                // Stop ray at hit position
                shotDistance = hit.distance;

                // Do damage to hit object where possible
                if (hit.collider.GetComponent<CarHealth>())
                {
                    hit.collider.GetComponent<CarHealth>().TakeDamage(10);
                }

                // Instantiate particle system on hit
                GameObject currentParticle = Instantiate(hitParticle, ray.GetPoint(shotDistance), Quaternion.identity);
                Destroy(currentParticle, 1.0f);
            }

            Debug.DrawRay(ray.origin, shotDistance * ray.direction, Color.red, 1);

        }

    }


    private bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime)
            canShoot = false;

        return canShoot;
    }

    IEnumerator RenderTracer(Vector3[] pos)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, pos[0]);
        tracer.SetPosition(1, pos[1]);
        yield return null;
        tracer.enabled = false;
    }

}


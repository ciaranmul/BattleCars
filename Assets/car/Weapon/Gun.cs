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

            if (Physics.Raycast(ray, out hit, shotDistance, collisionMask))
            {
                shotDistance = hit.distance;

                //if (hit.collider.GetComponent<Health>())
                //{
                //    hit.collider.GetComponent<Health>().TakeDamage(10);
                //}
                Debug.Log("Hit something");
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            if (tracer)
            {
                StartCoroutine("RenderTracer", shotDistance * ray.direction);
            }

            Debug.DrawRay(spawn.position, shotDistance * ray.direction, Color.red, 1);

            GetComponent<AudioSource>().Play();
        }

    }


    private bool CanShoot()
    {
        bool canShoot = true;

        if (Time.time < nextPossibleShootTime)
            canShoot = false;

        return canShoot;
    }

    IEnumerator RenderTracer(Vector3 hitPoint)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, spawn.position);
        tracer.SetPosition(1, hitPoint);
        yield return null;
        tracer.enabled = false;
    }
}


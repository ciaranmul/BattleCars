using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour {

    public float range = 1000;
    public LayerMask collisionMask;
    public float fireRate;
    public GameObject player;

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

                if (hit.collider.GetComponent<CarHealth>())
                {
                    hit.collider.GetComponent<CarHealth>().TakeDamage(10, player.GetComponent<CarGenerateInputStrings>().player);
                }
                Debug.Log("Hit something");
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;

            if (tracer)
            {
                Vector3[] parms = new Vector3[2] { ray.origin, ray.GetPoint(shotDistance) };
                StartCoroutine("RenderTracer", parms);
            }

            Debug.DrawRay(ray.origin, shotDistance * ray.direction, Color.red, 1);

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

    IEnumerator RenderTracer(Vector3[] pos)
    {
        tracer.enabled = true;
        tracer.SetPosition(0, pos[0]);
        tracer.SetPosition(1, pos[1]);
        yield return null;
        tracer.enabled = false;
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]
public class Gun : MonoBehaviour {

    public Transform spawn;
    public float shotDistance = 20;
    public float fireRate;

    private float secondsBetweenShots;
    private float nextPossibleShootTime;

	// Use this for initialization
	void Start () {
        secondsBetweenShots = 60 / fireRate;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Shoot()
    {
        if (CanShoot())
        {
            Ray ray = new Ray(spawn.position, spawn.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, shotDistance))
            {
                shotDistance = hit.distance;
            }

            nextPossibleShootTime = Time.time + secondsBetweenShots;

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
}


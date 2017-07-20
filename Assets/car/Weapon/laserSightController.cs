using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserSightController : MonoBehaviour {

    Ray lineOfSight;
    RaycastHit hit;

    private LineRenderer laser;
    public LayerMask collisionMask;
    public Transform spawn;
    public float range = 100;

    // Use this for initialization
    void Start () {
        lineOfSight = new Ray(spawn.position, spawn.forward);
        if ( GetComponent<LineRenderer>() )
        {
            laser = GetComponent<LineRenderer>();
        }
    }

    // Update is called once per frame
    void Update () {

        lineOfSight.origin = spawn.position;
        lineOfSight.direction = spawn.forward;
        Debug.DrawRay(lineOfSight.origin, range * lineOfSight.direction, Color.red, 1);

        if (Physics.Raycast(lineOfSight, out hit, range, collisionMask))
        {
            laser.SetPosition(1, Vector3.forward * hit.distance);
        }
        else
        {
            laser.SetPosition(1, Vector3.forward * range);
        }
        
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWeapon : MonoBehaviour {

    public float deadZone = 0.1f;
    public float rotationSpeed = 4f;
    public Gun gun;

    float turnValueHorizontal;
    float turnValueVertical;

    // Use this for initialization
    void Start () {
        		
	}
	
	// Update is called once per frame
	void Update () {
        // TODO: Get rotation of right stick and point the weapon that way.
        //       Weapon should rotate relative to the orientation of the main camera.
        turnValueHorizontal = 0.0f;
        turnValueVertical = 0.0f;

        float turnAxisHorizontal = Input.GetAxis(this.GetComponentInParent<CarGenerateInputStrings>().horizontalAim);
        float turnAxisVertical = Input.GetAxis(this.GetComponentInParent<CarGenerateInputStrings>().verticalAim);

        if (Mathf.Abs(turnAxisHorizontal) > deadZone)
        {
            turnValueHorizontal = turnAxisHorizontal;
        }

        if(Mathf.Abs(turnAxisVertical) > deadZone)
        {
            turnValueVertical = turnAxisVertical;
        }


		if (Input.GetButton(this.GetComponentInParent<CarGenerateInputStrings>().fire1))
        {
            // Fire projectile now
            gun.Shoot();
        }
	}

    private void FixedUpdate()
    {
        float rotationAngle = Mathf.Atan2(turnValueHorizontal, -turnValueVertical) * Mathf.Rad2Deg;
        if (Mathf.Abs(turnValueVertical) != 0 || Mathf.Abs(turnValueHorizontal) != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, rotationAngle, 0.0f), rotationSpeed * Time.deltaTime);
        }
    }

}

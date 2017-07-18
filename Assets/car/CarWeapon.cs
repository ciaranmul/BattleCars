using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWeapon : MonoBehaviour {

    float deadZone = 0.1f;

    float rotationSpeed = 4f;

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
            Debug.Log("horizontal movement detected");
            turnValueHorizontal = turnAxisHorizontal;
        }

        if(Mathf.Abs(turnAxisVertical) > deadZone)
        {
            Debug.Log("vertical movement detected");
            turnValueVertical = turnAxisVertical;
        }


		if (Input.GetButton(this.GetComponentInParent<CarGenerateInputStrings>().fire1))
        {
            // Fire projectile now
            Debug.Log(string.Format("Player {0} fired", this.GetComponentInParent<CarGenerateInputStrings>().player));
        }
	}

    private void FixedUpdate()
    {
        float rotationAngle = Mathf.Atan2(turnValueHorizontal, -turnValueVertical) * Mathf.Rad2Deg;
        Debug.Log(rotationAngle);
        if (Mathf.Abs(turnValueVertical) != 0 || Mathf.Abs(turnValueHorizontal) != 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, rotationAngle, 0.0f), rotationSpeed * Time.deltaTime);
        }
    }

}

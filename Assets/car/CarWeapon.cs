using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarWeapon : MonoBehaviour {


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        // TODO: Get rotation of right stick and point the weapon that way.
        //       Weapon should rotate relative to the orientation of the main camera.
		if (Input.GetButton(this.GetComponent<CarGenerateInputStrings>().fire1))
        {
            // Fire projectile now
            Debug.Log(string.Format("Player {0} fired", this.GetComponent<CarGenerateInputStrings>().player));
        }
	}
   
}

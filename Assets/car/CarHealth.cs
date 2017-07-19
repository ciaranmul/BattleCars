using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int startingLives = 3;
    public int carCollisionDamage = 10;
    public int currentLives;
    public int currentHealth;
    float hitTimer;

    bool hasNoLives = false;
    bool isCurrentlyColliding = false;

	// Use this for initialization
	void Start () {
        currentHealth = startingHealth;
        currentLives = startingLives;
	}
	
	// Update is called once per frame
	void Update () {
        hitTimer += Time.deltaTime;
	}

    //Method to deduct health from a player
    public void TakeDamage(int amount) {
        currentHealth -= amount;
        Debug.Log(string.Format("Player {0} has {1} health remaining", GetComponent<CarGenerateInputStrings>().player, currentHealth));

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    //Triggers when the car collides with another collision object
    void OnCollisionEnter(Collision other) {
        //On collision with another player car and more than 2 secs since last collision
        if (isCurrentlyColliding == false && other.collider.tag == "Car" && hitTimer > 2f)
        {
            //Gets speed of other car object
            var xValueColliding = Mathf.Abs(other.gameObject.GetComponent<Rigidbody>().velocity.x);
            var zValueColliding = Mathf.Abs(other.gameObject.GetComponent<Rigidbody>().velocity.z);
            var colliderSpeed = Mathf.Sqrt(Mathf.Pow(xValueColliding, 2) + Mathf.Pow(zValueColliding, 2));

            //Gets speed of current car object
            var xValue = Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.x);
            var zValue = Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.z);
            var speed = Mathf.Sqrt(Mathf.Pow(xValue, 2) + Mathf.Pow(zValue, 2));

            //Debug.Log(gameObject.name + "colliderSpeed: " + colliderSpeed + " speed: "  + speed);

            //If this cars speed is less than the other it recieves damage
            if (speed < colliderSpeed) {
                TakeDamage(carCollisionDamage);
            }

            //Resets hitTimer and isCurrentlyColliding to true to prevent more than 
            //one damage deduction per collision
            hitTimer = 0;
            isCurrentlyColliding = true;
        }

        //TODO: Complete collision with a projectile
        //On collision with a projectile it should take damage (May take damage value from projectile object variable)
        //as an example it is hard coded in here.

        /*elseif(other.collider.tag == "Projectile"){
         * TakeDamage(30)
         * Destroy(other.gameObject);
         * }
         */
    }

    //Triggers when the car collision ends
    void OnCollisionExit(Collision other) {
        isCurrentlyColliding = false;
    }
    
    //Respawns and deducts a player life if there are lives left 
    //if not do not repawn player
    void Death() {
        if (currentLives > 0)
        {
            currentLives -= 1;
            
            // TODO: player respawns
            currentHealth = startingHealth;
        }
        else
        {
            //TODO: player game over (no lives) 
            hasNoLives = true;
        }
    }

}

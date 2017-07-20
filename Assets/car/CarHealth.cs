using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CarHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int startingLives = 3;
    public int carCollisionDamage = 10;
    public int currentLives;
    public float currentHealth;
    float hitTimer;

    bool hasNoLives = false;
    bool isCurrentlyColliding = false;

    //Health UI elements
    public Image currentHealthbar;
    public Text ratioText;
    public Image[] containerImages;
    public Sprite[] healthSprites;

    //Player spawn location array
    public GameObject[] spawnLocations;
 
    // Use this for initialization
    void Start () {
        currentHealth = startingHealth;
        currentLives = startingLives;
        UpdateHeartContainers();
    }
	
	// Update is called once per frame
	void Update () {
        hitTimer += Time.deltaTime;
    }

    //Method to deduct health from a player
    public void TakeDamage(int amount) {
        currentHealth -= amount;
        //Debug.Log(string.Format("Player {0} has {1} health remaining", GetComponent<CarGenerateInputStrings>().player, currentHealth));
        UpdateHealthbar();

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

            //Player respawns after death if there are any lives left
            respawn();

        }
        else
        {
            //If player has no more call gameOver to destroy the player gameObject
            gameOver();
        }

        //Updates how many hearts are shown after a death
        UpdateHeartContainers();
    }

    //Updates the players healthbar to represent current health value 
    public void UpdateHealthbar()
    {
        float ratio = currentHealth / startingHealth;
        currentHealthbar.rectTransform.localScale = new Vector3(ratio, 1, 1);
        ratioText.text = (ratio * 100).ToString() + '%';
    }

    //Updates the players heart containers based on the number of lives left
    public void UpdateHeartContainers() {

        for (int i = 0; i < startingLives; i++)
        {
            if(currentLives-1 >= i)
            {
                containerImages[i].sprite = healthSprites[1];
            }
            else
            {
                containerImages[i].sprite = healthSprites[0];
            }
        }

    }

    public void respawn()
    {

        //Sets the payers location to be the same as one
        //of the gameObjects in the public array spawnLocations 
        this.transform.position = spawnLocations[Random.Range(0,spawnLocations.Length)].transform.position;
        
        //reset health to maximum and resets health bar after respawn
        currentHealth = startingHealth;
        UpdateHealthbar();
    }


    
    //TODO: Add more to gameover for a player (animation/particle effects)
    public void gameOver() {

        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class scoreTracker : MonoBehaviour {
    
    //Initialises kill count
    public int[] killCount;

    public GameObject[] players;
    public GameObject scoreBoard;

    void Start()
    {
        killCount = new int[4];

    }

    //Increments killCount
    public void incrementKillCount(int playerNum)
    {
        Debug.Log("Player Number: " + playerNum);
        Debug.Log("Array index: " + (playerNum-1));

        killCount[playerNum-1]++;
        Debug.Log("Player " + playerNum + "got a kill " + killCount[playerNum-1]);
    }

    public void CheckPlayersRemaining() {

        int playersRemaining = 0;

        for (int i = 0; i < players.Length; i++) {
            if (players[i].gameObject.activeSelf == true) {
                playersRemaining++;
                Debug.Log("Players Remaining:" + playersRemaining);
            }
        }

        if(playersRemaining <= 1)
        {
            Debug.Log("DEAD");
            scoreBoard.GetComponent<scoreBoard>().PopulateText(killCount);
            scoreBoard.SetActive(true);
                        
        }
        
    }
}

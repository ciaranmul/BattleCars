using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlayers : MonoBehaviour {

    public GameObject player3;
    public GameObject player4;

	// Use this for initialization
	void Start () {
        spawnPlayers();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void spawnPlayers()
    {
        Debug.Log(string.Format("PlayerPrefs.players = {0}", PlayerPrefs.GetInt("players")));
        if (PlayerPrefs.GetInt("players") >= 3)
        {
            player3.SetActive(true);
            if (PlayerPrefs.GetInt("players") == 4)
            {
                player4.SetActive(true);
            }
        }
    }
}

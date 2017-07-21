using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlayers : MonoBehaviour
{

    public GameObject[] players;
    public GameObject[] uis;
    private int numPlayers;

    // Use this for initialization
    void Start()
    {
        numPlayers = PlayerPrefs.GetInt("players");
        spawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnPlayers()
    {
        Debug.Log(string.Format("PlayerPrefs.players = {0}", numPlayers));
        for (int i = 0; i < numPlayers; i++)
        {
            players[i].SetActive(true);
            uis[i].SetActive(true);
        }
    }
}
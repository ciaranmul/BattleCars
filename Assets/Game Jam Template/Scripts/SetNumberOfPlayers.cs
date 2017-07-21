using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetNumberOfPlayers : MonoBehaviour
{


    public Dropdown playerChoice;

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.SetInt("players", 2);
        playerChoice.onValueChanged.AddListener(delegate
        {
            DropDownChange(playerChoice);
        });
    }

    void DropDownChange(Dropdown target)
    {
        PlayerPrefs.SetInt("players", target.value + 2);
        Debug.Log(string.Format("players = {0}", PlayerPrefs.GetInt("players")));
    }

    void Destroy()
    {
        playerChoice.onValueChanged.RemoveAllListeners();
    }

}
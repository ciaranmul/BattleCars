using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class scoreBoard : MonoBehaviour {

    public Text[] scoreText; 

  	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PopulateText(int[] scores) {
        for(int i = 0; i < scoreText.Length; i++)
        {
            scoreText[i].text = string.Format("Player {0}: {1}", i+1, scores[i]);
        }
    }
    

}

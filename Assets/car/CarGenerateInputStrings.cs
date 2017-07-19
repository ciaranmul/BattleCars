using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGenerateInputStrings : MonoBehaviour {
    public int player;

    public string accelerate;
    public string horizontal;
    public string vertical;
    public string horizontalAim;
    public string verticalAim;
    public string fire1;
    public string fire2;
    public string fire3;

    // Use this for initialization
    void Start () {
        generateInputStrings(player);	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void generateInputStrings(int player)
    {
        accelerate = string.Format("P{0}_Accelerate", player);
        horizontal = string.Format("P{0}_Horizontal", player);
        vertical = string.Format("P{0}_Vertical", player);
        horizontalAim = string.Format("P{0}_HorizontalAim", player);
        verticalAim = string.Format("P{0}_VerticalAim", player);
        fire1 = string.Format("P{0}_Fire1", player);
        fire2 = string.Format("P{0}_Fire2", player);
        fire3 = string.Format("P{0}_Fire3", player);
    }
}

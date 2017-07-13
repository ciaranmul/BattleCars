using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraBehaviour : MonoBehaviour {

    private Camera mainCamera;
    public GameObject target;
    public float cameraDistance = 20;

	// Use this for initialization
	void Start () {
        mainCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 targetPos = target.transform.transform.position;
        mainCamera.transform.position = new Vector3(targetPos.x, targetPos.y + cameraDistance, targetPos.z - cameraDistance/5);
	}
}

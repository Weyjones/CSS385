using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Display : MonoBehaviour {

    float CameraOrth = 100;

	// Use this for initialization
	void Start () {
        Camera mainCamera = GameObject.FindObjectOfType<Camera>();
        //set camera Orth
        mainCamera.orthographicSize = CameraOrth;

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

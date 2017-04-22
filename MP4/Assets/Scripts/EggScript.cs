using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggScript : MonoBehaviour {
    //init world bounds
    float CameraHeight;
    float CameraWidth;

    //init speed
    public float speed = 100;

    // Use this for initialization
    void Start() {
        //get world bounds
        CameraHeight = GameObject.FindObjectOfType<Camera>().orthographicSize;
        CameraWidth = GameObject.FindObjectOfType<Camera>().orthographicSize * GameObject.FindObjectOfType<Camera>().pixelWidth / GameObject.FindObjectOfType<Camera>().pixelHeight;

        //set velocity
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(GetComponent<Rigidbody2D>().rotation * Mathf.PI / 180), speed * Mathf.Sin(GetComponent<Rigidbody2D>().rotation * Mathf.PI / 180));
    }

    // Update is called once per frame
    void Update() {
        //if outside of the world bounds, destroy
        if (transform.position.x < -CameraWidth - 4 || transform.position.x > CameraWidth + 4 || transform.position.y < -CameraHeight - 4 || transform.position.y > CameraHeight + 4)
        {
            DestroyObject(gameObject);
        }
	}
}

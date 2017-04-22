using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour {

    //init world bounds
    float CameraHeight;
    float CameraWidth;

    //init movement variables
    public float maxSpeed = 20;
    public float rotationSpeed = 45f/60f;
    public float acceleration = 5f;
    private float speed = 0;

    //init egg prefab variables
    public GameObject Prefab;
    public float shotMaxTimer = .1f;
    float shotTimer = 0;

    //init egg text variables
    GameObject eggUI;
    Text eggText;
    int count;

    // Use this for initialization
    void Start () {

        //get world bounds
        CameraHeight = GameObject.FindObjectOfType<Camera>().orthographicSize;
        CameraWidth = GameObject.FindObjectOfType<Camera>().orthographicSize * GameObject.FindObjectOfType<Camera>().pixelWidth / GameObject.FindObjectOfType<Camera>().pixelHeight;

        //get egg text to update
        eggUI = GameObject.Find("EggText");
        eggText = eggUI.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update () {

        //up up is pressed or down is pressed, accelerate to max speed
        if (Input.GetKey(KeyCode.UpArrow))
        {
            speed += acceleration;
            if (speed > maxSpeed)
                speed = maxSpeed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            speed -= acceleration;
            if (speed < -maxSpeed)
                speed = -maxSpeed;
        }
        else //else decrement speed back to 0
        {
            if (speed < 0)
            {
                speed += acceleration;
                if (speed > 0)
                    speed = 0;
            }
            else if (speed > 0)
            {
                speed -= acceleration;
                if (speed < 0)
                    speed = 0;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow)) //if left arrow is pressed, rotate left
        {
            transform.Rotate(0f, 0f, rotationSpeed);
        }

        if (Input.GetKey(KeyCode.RightArrow)) //if right arrow is pressed, rotate right
        {
            transform.Rotate(0, 0, -rotationSpeed);
        }

        if (Input.GetKey(KeyCode.LeftControl)) //if Left Control is pressed, create new egg
        {
            shotTimer += Time.deltaTime; //inc timer
            if (shotTimer > shotMaxTimer) //if timer reaches shot max timer, spawn new egg and reset timer
            {
                Instantiate(Prefab, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
                shotTimer = 0;
            }
        }

        //update velocity based on current speed and rotation
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(GetComponent<Rigidbody2D>().rotation * Mathf.PI / 180), speed * Mathf.Sin(GetComponent<Rigidbody2D>().rotation * Mathf.PI / 180));

        //if outside the world bounds, translate to other side
        if (transform.position.x < -CameraWidth - 3)
        {
            transform.position = new Vector3(CameraWidth + 3, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > CameraWidth + 3)
        {
            transform.position = new Vector3(-CameraWidth - 3, transform.position.y, transform.position.z);
        }
        else if (transform.position.y < -CameraHeight - 3)
        {
            transform.position = new Vector3(transform.position.x, CameraHeight + 3, transform.position.z);
        }
        else if (transform.position.y > CameraHeight + 3)
        {
            transform.position = new Vector3(transform.position.x, -CameraHeight - 3, transform.position.z);
        }

        //update egg count text
        count = GameObject.FindGameObjectsWithTag("Egg").Length;
        eggText.text = "Egg Count: " + count;
    }
}

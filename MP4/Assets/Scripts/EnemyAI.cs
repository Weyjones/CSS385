using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    //init camera bounds
    float CameraHeight;
    float CameraWidth;
    //init speed variables
    float speed;
    float saveSpeed;

    //init Egg array to keep track of active eggs
    GameObject[] Eggs;
    public AudioSource hitSound;
    //init player varaible to track hit box collision
    GameObject player;

    //init stun variables
    public float stunTime = 5f;
    float stunDuration = 0f;
    int timesStunned = 0;
    public int maxStuns = 3;

    //init state enum
    enum state { Normal, Run, Stunned };
    state currentState;

    //init isMoving bool and turn speed
    private bool isMoving = true;
    public float turnSpeed = -.035f;
    //public AudioSource deathSound;

    // Use this for initialization
    void Start () {
        //get world bounds
        CameraHeight = GameObject.FindObjectOfType<Camera>().orthographicSize;
        CameraWidth = GameObject.FindObjectOfType<Camera>().orthographicSize * GameObject.FindObjectOfType<Camera>().pixelWidth / GameObject.FindObjectOfType<Camera>().pixelHeight;
        
        //get random speed and set initial stopped state
        saveSpeed = Random.Range(20f, 40f);
        speed = 0f;
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        currentState = state.Normal;
        hitSound = GetComponent<AudioSource>();

        //get player object
        player = GameObject.FindGameObjectWithTag("Hero");

        //Load Death Sound
        //deathSound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        //get all eggs
        Eggs = GameObject.FindGameObjectsWithTag("Egg");

        //if space is pressed, toggle isMoving
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if (isMoving)
        //    {
        //        isMoving = false;
        //    }
        //    else
        //    {
        //        isMoving = true;
        //    }
        //}
        
        if (currentState != state.Stunned)
        {
            //if not in stunned state, update speed based on state
            if (!isMoving)
            {
                speed = 0f;
            }
            else
            {
                speed = saveSpeed;
            }

            //check collision with player and run if within 30 units
            if (this.GetComponent<CircleCollider2D>().IsTouching(player.GetComponent<BoxCollider2D>()))
            {
                currentState = state.Run;
            }
            else
            {
                currentState = state.Normal;
            }
        }

        //check collision with all eggs
        for(int i = 0; i < Eggs.Length; i++)
        {
            if (GetComponent<BoxCollider2D>().IsTouching(Eggs[i].GetComponent<BoxCollider2D>())){
                //if touching check if enemy has been stunned less than max stuns
                if (timesStunned < maxStuns) //if less, stun the enemy and destroy the egg
                {
                    currentState = state.Stunned;
                    DestroyObject(Eggs[i]);
                    hitSound.Play();
                    timesStunned++;
                    stunDuration = 0f;
                    speed = 0;
                }
                else //else destroy the enemy
                {
                //    deathSound.Play();
                    destroyEnemy();
                    DestroyObject(Eggs[i]);
                }
            }
        }

        switch (currentState) 
        {
            case state.Normal: //if normal state, enable normal state texture
                transform.FindChild("Chaser_Normal").gameObject.SetActive(true);
                transform.FindChild("Chaser_Run").gameObject.SetActive(false);
                transform.FindChild("Chaser_Stunned").gameObject.SetActive(false);
                break;
            case state.Run: //if run state, enable run texture
                transform.FindChild("Chaser_Normal").gameObject.SetActive(false);
                transform.FindChild("Chaser_Run").gameObject.SetActive(true);
                transform.FindChild("Chaser_Stunned").gameObject.SetActive(false);
                if (isMoving) //if moving, rotate away from player
                {
                    Vector3 dir = player.transform.position - transform.position;
                    Vector3 forward = GetComponent<Rigidbody2D>().velocity;
                    forward.Normalize();
                    dir.Normalize();

                    float cosTheta = Vector2.Dot(dir, forward); //get angle

                    Vector3 cross = Vector3.Cross(forward, dir);

                    float rad = Mathf.Acos(cosTheta);  //get radian rotation

                    if (cross.z < 0) //change direction based on Z value
                    {
                        rad = -rad;
                    }
                    rad *= turnSpeed; //lower rotation to 5% of angle

                    transform.Rotate(0, 0, rad * 180 / Mathf.PI); //rotate

                }
                break;
            case state.Stunned: //if stunned set texture to stunned
                transform.FindChild("Chaser_Normal").gameObject.SetActive(false);
                transform.FindChild("Chaser_Run").gameObject.SetActive(false);
                transform.FindChild("Chaser_Stunned").gameObject.SetActive(true);

                //inc stun duration
                stunDuration += Time.deltaTime;
 
                if(stunDuration > stunTime) //if stun duration reached, set state to normal
                {
                    currentState = state.Normal;
                    speed = saveSpeed;
                }
                else //else rotate
                {
                    transform.Rotate(0, 0, 9f/60f);
                }
                break;
            default:
                Debug.LogError("EnemyAI in bad state");
                break;
        }

        //update velocity based on current rotaion and speed
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Mathf.Cos(GetComponent<Rigidbody2D>().rotation * Mathf.PI/180), speed * Mathf.Sin(GetComponent<Rigidbody2D>().rotation * Mathf.PI / 180));

        //if exiting world bounds, tranlate to opposite side
        if(transform.position.x < -CameraWidth - 10)
        {
            transform.position = new Vector3(CameraWidth + 9, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > CameraWidth + 10)
        {
            transform.position = new Vector3(-CameraWidth - 9, transform.position.y, transform.position.z);
        }
        else if(transform.position.y < -CameraHeight - 10)
        {
            transform.position = new Vector3(transform.position.x, CameraHeight + 9, transform.position.z);
        }
        else if(transform.position.y > CameraHeight + 10)
        {
            transform.position = new Vector3(transform.position.x, -CameraHeight - 9, transform.position.z);
        }

    }

    private void destroyEnemy()
    {
        //death_sound.Play(); // Play Death sound
        Destroy(gameObject); //destroy enemy
    }

    public void setHealth(int health)
    {
        maxStuns = health;
    }
}

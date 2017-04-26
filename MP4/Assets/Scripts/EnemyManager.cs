using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {

    //init camera bounds
    float CameraHeight;
    float CameraWidth;

    //init enemy spawn variables
    //public int enemyStartCount = 50;
    //public float spawnTimer = 10;
    //float timer = 0;

    //init enemy spawn varaibles
    public GameObject enemy;
    public bool isMoving = false;
    public int count;

    //AudioSource tools
    private int previousCount;
    public AudioSource deathSound;

    //init enemy count text
    GameObject scoreUI;
    Text scoreText;

    public int EnemyCount;
    public int EnemyHealth;

    // Use this for initialization
    void Start()
    {
        //get camera bounda
        CameraHeight = GameObject.FindObjectOfType<Camera>().orthographicSize;
        CameraWidth = GameObject.FindObjectOfType<Camera>().orthographicSize * GameObject.FindObjectOfType<Camera>().pixelWidth / GameObject.FindObjectOfType<Camera>().pixelHeight;

        //get EnemyText to update
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Menu"))
        {
            scoreUI = GameObject.Find("EnemyText");
            scoreText = scoreUI.GetComponent<Text>();
        }
        deathSound = GetComponent<AudioSource>();
        CreateWave(EnemyCount, EnemyHealth);
        previousCount = EnemyCount;
    }

    // Update is called once per frame
    void Update() {

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
        ////if isMoving, spawn enemies at set time
        //if (isMoving)
        //{
        //    timer += Time.deltaTime; //inc time

        //    if (timer > spawnTimer) //if larger than spawn timer, create new enemy and reset timer
        //    {
        //        GameObject en = Instantiate(enemy, new Vector3(Random.Range(-CameraWidth, CameraWidth), Random.Range(-CameraHeight, CameraHeight), 0), Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
        //        en.GetComponent<EnemyAI>().isMoving = isMoving;
        //        en.transform.parent = transform;
        //        timer = 0;
        //    }
        //}
        //else
        //    timer = 0;

        count = transform.childCount; //get count of children
        if (count < previousCount)
        {
            deathSound.Play();
            previousCount = count;
        }
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Menu"))
            scoreText.text = "Enemy Count: " + count; //update enemy text with count
    }

    public void CreateWave(int num, int health)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject en = Instantiate(enemy, new Vector3(Random.Range(-CameraWidth, CameraWidth), Random.Range(-CameraHeight, CameraHeight), 0), Quaternion.Euler(0, 0, Random.Range(0f, 359f)));
            en.GetComponent<EnemyAI>().setHealth(health);
            en.transform.parent = transform; //set parent to keep track of them
        }
    }

    public int getEnemyCount()
    {
        return count;
    }
}

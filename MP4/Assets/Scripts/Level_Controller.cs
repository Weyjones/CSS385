using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_Controller : MonoBehaviour {

    private EnemyManager EM;

    public int level;

    private int maxLevel = 2;

    private int count;

    public bool playing = false;

    private float levelTime = 0f;

    //Text for the end of the level
    public Text endLevelTitle;
    public Text endLevelDescription;

	// Use this for initialization
	void Start () {
        EM = FindObjectOfType<EnemyManager>();

        if (endLevelDescription != null && endLevelTitle != null)
        {
            endLevelTitle.enabled = false;
            endLevelDescription.enabled = false;
        } else
        {

            Debug.Log("Text does not exist");
        }
        
}
	
	// Update is called once per frame
	void Update () {

        if (playing)
            levelTime += Time.smoothDeltaTime;

        count = EM.getEnemyCount();

        if (playing == true)
        {
            

            if (count < 1)
            {
                if (level < maxLevel)
                {
                    level++;
                    startLevel();
                }
                else
                {
                    playing = false;
                    StartCoroutine(EndLevel("Menu"));
                }
            }
        }

        if(count > 0)
        {
            playing = true;
        }
        

        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadSceneAsync("Menu");
            playing = false;
        }
    }

    void startLevel()
    {
        //Todo, add a wait timer between wave?

        switch (level)
        {
            case 1:
                SceneManager.LoadSceneAsync("Level_1");
                playing = false;
                break;
            case 2:
                
                playing = false;
                StartCoroutine(EndLevel("Level_2"));
                
                break;
        }
    }

    IEnumerator EndLevel(string levelName)
    {
        
        endLevelTitle.enabled = true;
        string victoryText = "";
        if (levelName == "Level_2")
        {
            victoryText = "LEVEL I COMPLETE";
        } else
        {
            victoryText = "LEVEL II COMPLETE";
        }
        
        endLevelTitle.text = victoryText;
        
        
        yield return new WaitForSeconds(1);


        
        endLevelDescription.enabled = true;

        float time = Mathf.Round((levelTime * 1000f)) / 1000f;

        float enemyRatio = EM.totalEnemyCount / levelTime;
        enemyRatio = Mathf.Round((enemyRatio * 1000f)) / 1000f;

        endLevelDescription.text = "You won by dismissing " +
            EM.totalEnemyCount + " faces in " + time + " seconds!" +
            "\n" + "\n" + "Your score/ratio is " + enemyRatio + 
            " smiles dismissed per second!";
        yield return new WaitForSeconds(4);

        if (levelName != "Menu")
        {
            endLevelDescription.text = "Starting next level in three...";
            yield return new WaitForSeconds(1.5f);
            endLevelDescription.text = "Next level in two...";
            yield return new WaitForSeconds(1.5f);
            endLevelDescription.text = "Next level in one!";
            yield return new WaitForSeconds(1f);

            SceneManager.LoadSceneAsync(levelName);
        }
        else
        {
            endLevelDescription.text = "Returning to main menu in three...";
            yield return new WaitForSeconds(1.5f);
            endLevelDescription.text = "Main menu in two...";
            yield return new WaitForSeconds(1.5f);
            endLevelDescription.text = "Main menu in one!";
            yield return new WaitForSeconds(1f);

            SceneManager.LoadSceneAsync(levelName);
        }
        
    }

    /*
    void startLevel()
    {
        //Todo, add a wait timer between wave?

        switch (level)
        {
            case 1:
                SceneManager.LoadSceneAsync("Level_1");
                playing = false;
                break;
            case 2:
                SceneManager.LoadSceneAsync("Level_2");
                playing = false;
                break;
        }
    }*/

    public void StartPlaying()
    {
        playing = true;
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Controller : MonoBehaviour {

    private EnemyManager EM;

    public int level;

    private int maxLevel = 2;

    private int count;

    public bool playing = false;

	// Use this for initialization
	void Start () {
        EM = FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {

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
                    //Todo End Game, return to menu
                }
            }
        }

        if(count > 0)
        {
            playing = true;
        }

        //debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playing = true;
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
                SceneManager.LoadSceneAsync("Level_2");
                playing = false;
                break;
        }
    }
}

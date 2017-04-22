using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Controller : MonoBehaviour {

    private EnemyManager EM;

    private int level = 0;

    private int maxLevel = 2;

    private int count;

    public GameObject Hero;

    bool playing = false;

	// Use this for initialization
	void Start () {
        EM = FindObjectOfType<EnemyManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (playing == true)
        {
            count = EM.getEnemyCount();

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

        //debug
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playing = true;
        }
	}

    void startLevel()
    {
        //Todo, add a wait timer between wave?

        Hero.transform.position = new Vector3(0, 0, 0);

        switch (level)
        {
            case 1:
                EM.CreateWave(5, 3);
                break;
            case 2:
                EM.CreateWave(10, 5);
                break;
        }
    }
}

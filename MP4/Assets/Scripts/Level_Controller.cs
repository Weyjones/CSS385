using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Controller : MonoBehaviour {

    private EnemyManager EM = new EnemyManager();

    private int level = 1;

    private int maxLevel = 2;

    private int count;

    public GameObject Hero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count = EM.getEnemyCount();
        
        if(count < 1)
        {
            if(level < maxLevel)
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

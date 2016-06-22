﻿using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    [SerializeField]
    public static float score;


    public float moveSpeed;


    public enum gameState
    {
        normalPlay, //Simple spawning of enemies, no bosses
        bossFight, //Currently in bossFight
    }

    public gameState state;
    

    void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        state = gameState.normalPlay;

        score = 0;
    }

    void Update()
    {
        if(EnemySpawnManager.current.totalEnemiesSpawned == 10)
        {
            EnemySpawnManager.current.spawnEnemies = false;
            
        }
        if(EnemySpawnManager.current.totalEnemiesSpawned == 10 && EnemySpawnManager.currentEnemies == 0)
        {
            EnemySpawnManager.current.spawnBoss = true;
           

        }
    }

    public static void KillEnemy(Enemy enemy)
    {
       
        if(enemy.tag == "Boss")
        {

            enemy.gameObject.SetActive(false);
            EnemySpawnManager.currentBosses--;
            EnemySpawnManager.bossEnemy = null;
            BossUI.current.bossGuiAnim.SetBool("ShowBossHealth", false);
            BossUI.current.bossGuiAnim.SetBool("Normal", true);
            score += enemy.stats.awardPoints;

            EnemySpawnManager.current.totalEnemiesSpawned = 0;
            EnemySpawnManager.current.spawnEnemies = true;





        }
        else
        {           
            enemy.gameObject.SetActive(false);
            EnemySpawnManager.currentEnemies--;
            score += enemy.stats.awardPoints;

        }
 
    }







	
}

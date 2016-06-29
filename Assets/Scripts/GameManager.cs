using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager gm;

    [SerializeField]
    public GameObject[] weaponList;
   

    [SerializeField]
    public static float score;


    public float moveSpeed;
    private bool occupied = false; //for IENumerator;


    public enum gameState
    {
        test, //Testing purposes
        setup,//Setup phase (upgrades, etc)
        ready, //state lasts 5 seconds before moving to normalPlay
        normalPlay, //Simple spawning of enemies, no bosses
        bossFight, //Currently in bossFight
        waiting //For any transitions into normal gameplay
    }

    public gameState state;
    

    void Start()
    {
        Application.targetFrameRate = 60;

        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }

        state = gameState.setup;

        score = 0;
    }

    void Update()
    {
        if (Input.GetKey("r"))
        {
            state = gameState.normalPlay;
        }
        if (EnemySpawnManager.current.totalEnemiesSpawned == 10)
        {
            EnemySpawnManager.current.spawnEnemies = false;
            
        }
        if(EnemySpawnManager.current.totalEnemiesSpawned == 10 && EnemySpawnManager.currentEnemies == 0)
        {
            EnemySpawnManager.current.spawnBoss = true;
        }

        if(state == gameState.ready)
        {
            if (!occupied)
            {
                StartCoroutine(readyState());
            }
        }

        if (state == gameState.waiting)
        {
            if (!occupied)
            {
                StartCoroutine(waitState());
            }
        }
    }

    public static void KillEnemy(Enemy enemy)
    {
       
        if(enemy.tag == "Boss")
        {

            enemy.gameObject.SetActive(false);
            EnemySpawnManager.currentBosses--;
            EnemySpawnManager.bossEnemy = null;
            BossUI.current.bossGuiAnim.enabled = true;
            BossUI.current.bossGuiAnim.SetBool("BossFightReady", false);
            BossUI.current.bossGuiAnim.SetBool("Normal", true);
            score += enemy.stats.awardPoints;

            GameManager.gm.state = gameState.waiting;

        }
        else
        {           
            enemy.gameObject.SetActive(false);
            EnemySpawnManager.currentEnemies--;
            score += enemy.stats.awardPoints;

        }
 
    }

    public IEnumerator readyState() //Transitioning after Setup state into NormalPlay;
    {
        occupied = true;
        yield return new WaitForSeconds(5f);
        state = gameState.normalPlay;
        occupied = false;
    }

    public IEnumerator waitState() //Transitioning after BossFight state into Setup;
    {
        occupied = true;
        yield return new WaitForSeconds(5f);
        state = gameState.setup;
        occupied = false;
    }








}

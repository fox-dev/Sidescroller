using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public bool debug;

    public GameObject particle;

    [System.Serializable]
    public class GameStats //Keep track of player's stats for ranking/grade
    {
        public int roundScore; //points accumulated during the round
        public int enemiesDestroyed; //number of enemies destroyed;
        public int enemiesMissed; //Number of enemies not killed/escaped
        public int totalDamageDone; //Amount of damage the player has done
        public int timesHit; //number of times the player was hit
        public int totalDamageTaken; //Amount of damage the player has taken

        public void init()
        {
            roundScore = timesHit = enemiesDestroyed = enemiesMissed = totalDamageDone = 0; //initialize all stats to 0
        }


        /////Increment methods/functions//////
        public void addRoundScore(int amount)
        {
            roundScore += amount;
        }

        public void addTimesHit()
        {
            timesHit += 1;
        }

        public void addEnemiesDestroyed()
        {
            enemiesDestroyed += 1;
        }

        public void addEnemiesMissed()
        {
            enemiesMissed += 1;
        }

        public void addTotalDamageDone(int amount)
        {
            totalDamageDone += amount;
        }

        public void addTotalDamageTaken(int amount)
        {
            totalDamageTaken += amount;
        }
        /////////////////////////////////////
    }

    //Enum for Game States
    public enum gameState
    {
        test, //Testing purposes
        menu, //Menu state for main menu
        tutorial_1, tutorial_2, tutorial_3, //Tutorial phases 
        setup,//Setup phase (upgrades, etc)
        results, //Results phase to show round results
        ready, //state lasts 5 seconds before moving to normalPlay
        normalPlay, //Simple spawning of enemies, no bosses
        prepareForBoss, //state lasts a bit before transitioning to bossFight
        bossFight, //Currently in bossFight
        waiting //For any transitions into normal gameplay
    }

    public static GameManager gm; //Static variable single instance of GameManager reference

    [SerializeField]
    public GameObject[] weaponList; //List of all possible weapons in the game, used for upgrading and equipping weapons; 

    [SerializeField]
    public static float score;

    [SerializeField]
    public static float currency;

    public gameState state;

    public GameStats gameStats = new GameStats();

    public float moveSpeed; //adjust value to change moveSpeed of origin objects
    private bool occupied = false; //Used for ienumerator calls in Update method


    void Awake()
    {
        Application.targetFrameRate = 60;
   
        gm = this;

        state = gameState.menu;

        score = 0;
        currency = 10000;

        
        gameStats.init();
    }

    void Update()
    {
        if (Input.GetKey("r"))
        {
            state = gameState.normalPlay;
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

    public static void CollectCurrency(Currency item)
    {
        item.gameObject.SetActive(false);
        currency += item.amount;

        //Update UI currency text
        CurrencyUI.current.UpdateText();
    }

    public static void SubtractCurrency(int amount)
    {
        if (!gm.debug)
        {
            currency -= amount;
        }

         //Update UI currency text
        CurrencyUI.current.UpdateText();

    }

    public static void KillEnemy(Enemy enemy)
    {
        GameObject hitEffect = ObjectPool.current.getPooledObject(gm.particle);

        if (hitEffect == null) return;
        hitEffect.transform.position = enemy.transform.position;

        hitEffect.SetActive(true);

        if(enemy.tag == "Tutorial_Enemy")
        {
            if (GameManager.gm.state == GameManager.gameState.tutorial_2)
            {
                TutorialOverlayUI.current.oneKilled();
                enemy.gameObject.SetActive(false);
                EnemySpawnManager.currentEnemies--;
            }

            if (GameManager.gm.state == GameManager.gameState.tutorial_3)
            {
                TutorialOverlayUI.current.twoKilled();
                enemy.gameObject.SetActive(false);
                EnemySpawnManager.currentEnemies--;
                EnemySpawnManager.current.spawnEnemies = false;
            }
        }
        else if (enemy.tag == "Boss")
        {

            enemy.gameObject.SetActive(false);
            EnemySpawnManager.currentBosses--;
            EnemySpawnManager.bossEnemy = null;
            BossUI.current.bossGuiAnim.enabled = true;
            BossUI.current.bossGuiAnim.SetBool("BossFightReady", false);
            BossUI.current.bossGuiAnim.SetBool("Normal", true);
            score += enemy.stats.awardPoints;

            //Update UI score text
            ScoreUI.current.UpdateText();

            //Increment appropriate stats in gameStates//
            gm.gameStats.addRoundScore(enemy.stats.awardPoints);
            gm.gameStats.addEnemiesDestroyed();
            /////////////////////////////////////////////

            GameManager.gm.state = gameState.waiting;

        }
        else
        {
            enemy.gameObject.SetActive(false);
            EnemySpawnManager.currentEnemies--;
            score += enemy.stats.awardPoints;

            //Update UI score text
            ScoreUI.current.UpdateText();

            //Increment appropriate stats in gameStates//
            gm.gameStats.addRoundScore(enemy.stats.awardPoints);
            gm.gameStats.addEnemiesDestroyed();
            /////////////////////////////////////////////
        }

    }

    public static void disableEnemy(Enemy enemy) //used to disable enemies that move offscreen
    {
        enemy.gameObject.SetActive(false);
        EnemySpawnManager.currentEnemies--;

        //Increment appropriate stats in gameStates//
        gm.gameStats.addEnemiesMissed();

    }

    public IEnumerator readyState() //Transitioning after Setup state into NormalPlay;
    {

        occupied = true;
        gameStats.init();
        yield return new WaitForSeconds(0.5f);
        GetReadyGUI.current.getReadyGUIAnim.SetBool("GetReady", true);
        yield return new WaitForSeconds(2f);
        GetReadyGUI.current.getReadyGUIAnim.SetBool("GetReady", false);
        state = gameState.normalPlay;
        occupied = false;
    }

    public IEnumerator waitState() //Transitioning after BossFight state into Setup;
    {
        
        occupied = true;
        EnemySpawnManager.current.reinit();
        yield return new WaitForSeconds(5f);
        state = gameState.results;
        occupied = false;
    }








}

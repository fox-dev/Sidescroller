using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private AdController adController;

	public bool debug;

    public GameObject particle_Currency, particle_Hit, floatText; //Currency sprite, hit particle for screen clears, and float collecting text;

    private GameObject player;

	public static int difficulty;

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

        //The current round's score//
        public void addRoundScore(int amount)
        {
            roundScore += amount;
        }

        public void addToTotalScore(int amount)
        {
            score += amount;

            if(score > highScore)
            {
                highScore = score;

                HighscoreUI.current.UpdateText();
            }
        }


        ///////////////////////////////////////
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

    [System.Serializable]
    public class Upgrades //Keep track of player's available upgrades
    {
        public bool fireBall_x3;
        public bool buddy;
        public bool pierceBeam;
        public bool rocketRate;



        public void init() //Change to init based off player prefs 
        {
            fireBall_x3 = buddy = pierceBeam = rocketRate = false;
        }
        //enable methods
        public void enablePierceBeam() { pierceBeam = true; }
        public void enableFireBall_x3() { fireBall_x3 = true; }
        public void enableRocketRate() { rocketRate = true; }
        public void enableBuddy()
        {
            buddy = true;

            foreach (Transform child in gm.player.transform)
            {
                if (child.name == "Buddy")
                {
                   child.gameObject.SetActive(true);
                }
            }
        }

        //disable methods
        public void disablePierceBeam() { pierceBeam = false; }
        public void disableFireBall_x3() { fireBall_x3 = false; }
        public void disableRocketRate() { rocketRate = false; }
        public void disableBuddy()
        {
            buddy = false;
            foreach (Transform child in gm.player.transform)
            {
                if (child.name == "Buddy")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        public void disableAllUpgrades()
        {
            disablePierceBeam();
            disableFireBall_x3();
            disableRocketRate();
            disableBuddy();
        }

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
        waiting, //For any transitions into normal gameplay
        gameOver //GameOver state
    }

    public static GameManager gm; //Static variable single instance of GameManager reference

    [SerializeField]
    public GameObject[] weaponList; //List of all possible weapons in the game, used for upgrading and equipping weapons; 

    [SerializeField]
    public static int score;
    [SerializeField]
    public static int highScore;

    [SerializeField]
    public static int currency;

    public gameState state;
    public gameState prevState;

    public GameStats gameStats = new GameStats();

    public Upgrades upgrades = new Upgrades();

    public float moveSpeed; //adjust value to change moveSpeed of origin objects
    private bool occupied = false; //Used for ienumerator calls in Update method


	void Start()
	{
		GameObject adControllerObject = GameObject.FindWithTag("AdController");
		if (adControllerObject != null)
		{
			adController = adControllerObject.GetComponent<AdController>();
		}
		if (adController == null)
		{
			Debug.Log("Cannot find 'AdController' script");
		}

		difficulty = 0;
    }

	void Awake()
    {
        Application.targetFrameRate = 60;

        if (gm != null)
        {
            Debug.LogError("More than one GameManager in scene");
        }
        else
        {
            gm = this;
        }

        player = GameObject.FindGameObjectWithTag("Player");

        state = gameState.menu;

        LoadPrefs();
        //currency = 10000;

		difficulty = 0;
        
		gameStats.init();
        upgrades.init();
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

        GameObject collectText = ObjectPool.current.getPooledObject(gm.floatText);

        if (collectText == null) return;

        collectText.transform.position = gm.player.transform.position;
        collectText.GetComponent<FloatingText>().setText("+" + item.amount.ToString());
        collectText.SetActive(true); //Play currency collection text animation

        currency += item.amount;

        PlayerPrefs.SetInt("Currency", currency);

        AudioManager.current.PlaySound("Collect");

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

    public static void KillPlayer(Player player)
    {
        player.gameObject.SetActive(false);
        gm.prevState = gm.state;
        gm.state = gameState.gameOver;
        AudioManager.current.delayPlaySound("GameOver", 0.5f);

		if(gm.adController.AdFlag())
		{
			gm.StartCoroutine(gm.showAd (2f));

			gm.adController.showBannerAd ();
		}
    }

    public static void KillEnemy(Enemy enemy)
    {
        
  
     
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
            GameObject currency = ObjectPool.current.getPooledObject(gm.particle_Currency); //Spawn currency
            if (currency == null) return;
            currency.transform.position = enemy.transform.position;

            currency.GetComponent<Currency>().amount = 1000;

            enemy.gameObject.SetActive(false);
            EnemySpawnManager.currentBosses--;
            EnemySpawnManager.bossEnemy = null;
            BossUI.current.bossGuiAnim.enabled = true;
            BossUI.current.bossGuiAnim.SetBool("BossFightReady", false);
            BossUI.current.bossGuiAnim.SetBool("Normal", true);
            //score += enemy.stats.awardPoints;
            gm.gameStats.addToTotalScore(enemy.stats.awardPoints);

            //Update UI score text
            ScoreUI.current.UpdateText();

            //Increment appropriate stats in gameStates//
            gm.gameStats.addRoundScore(enemy.stats.awardPoints);
            gm.gameStats.addEnemiesDestroyed();
            /////////////////////////////////////////////

            GameManager.gm.state = gameState.waiting;
			difficulty++;

			print ("difficulty level: " + difficulty);

            currency.SetActive(true);

        }
        else
        {
            
            enemy.gameObject.SetActive(false);
            EnemySpawnManager.currentEnemies--;
            //score += enemy.stats.awardPoints;
            gm.gameStats.addToTotalScore(enemy.stats.awardPoints);

            //Update UI score text
            ScoreUI.current.UpdateText();

            //Increment appropriate stats in gameStates//
            gm.gameStats.addRoundScore(enemy.stats.awardPoints);
            gm.gameStats.addEnemiesDestroyed();
            /////////////////////////////////////////////

            int rand = Random.Range(0, 5);
            if (rand == 2) //Spawn
            {
                GameObject currency = ObjectPool.current.getPooledObject(gm.particle_Currency); //Spawn currency
                if (currency == null) return;
                currency.transform.position = enemy.transform.position;

                currency.SetActive(true);
            }
        }

        

    }

    public static void disableEnemy(Enemy enemy) //used to disable enemies that move offscreen
    {
        enemy.gameObject.SetActive(false);
        EnemySpawnManager.currentEnemies--;

        //Increment appropriate stats in gameStates//
        gm.gameStats.addEnemiesMissed();

    }

    public static void respawnPlayer()
    {
        AudioManager.current.PlaySound("Respawn");
        AudioManager.current.PlaySound("Spawn");

        clearScreenOfProjectiles();
        gm.player.SetActive(true);
        gm.player.GetComponent<Player>().respawn();
    }

    public static void clearScreenOfEnemies()
    {
        GameObject[] listOfObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in listOfObjects)
        {

            if (obj.activeInHierarchy)
            {
                if (obj.layer == LayerMask.NameToLayer("Enemy"))
                {
                    obj.SetActive(false);
                }

                if (obj.layer == LayerMask.NameToLayer("Projectile"))
                {
                    obj.SetActive(false);
                }

            }
        }
		EnemySpawnManager.current.resetEnemyCounters();
    }

    public static void clearScreenOfProjectiles()
    {
        GameObject[] listOfObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in listOfObjects)
        {

            if (obj.activeInHierarchy)
            {
                if (obj.layer == LayerMask.NameToLayer("Projectile"))
                {
                    GameObject effect = ObjectPool.current.getPooledObject(gm.particle_Hit);

                    if (effect == null) return;

                    effect.transform.position = obj.transform.position;
                    effect.transform.rotation = obj.transform.rotation;
                    effect.SetActive(true);


                    obj.SetActive(false);
                }

            }
        }
    }

    public static void resetBossFlags()
    {
        EnemySpawnManager.currentBosses--;
        EnemySpawnManager.bossEnemy = null;
        BossUI.current.bossGuiAnim.enabled = true;
        BossUI.current.bossGuiAnim.SetBool("BossFightReady", false);
        BossUI.current.bossGuiAnim.SetBool("Normal", true);
    }

	public static void turnOffAds()
	{

		gm.adController.hideBannerAd ();
		
	}

	public static void getNewAds()
	{
		if (gm.adController.AdFlag ()) {
			gm.adController.requestNewBannerAd ();
			gm.adController.requestNewInterstitialAd ();
		}
	}

    public IEnumerator readyState() //Transitioning after Setup state into NormalPlay;
    {
         
        occupied = true;
        gameStats.init();
        yield return new WaitForSeconds(0.5f);
        GetReadyGUI.current.getReadyGUIAnim.SetBool("GetReady", true);
        AudioManager.current.PlaySound("Ready");
        yield return new WaitForSeconds(1.5f);
        GetReadyGUI.current.getReadyGUIAnim.SetBool("GetReady", false);
        state = gameState.normalPlay;
        occupied = false;
    }

    public IEnumerator waitState() //Transitioning after BossFight state into Setup;
    {
        
        occupied = true;
        savePref();
        EnemySpawnManager.current.reinit();
        yield return new WaitForSeconds(5f);
        state = gameState.results;
        occupied = false;
    }

	public IEnumerator showAd(float time)
	{
		yield return new WaitForSeconds (time);

		if (gm.adController.adIsLoaded ()) {
			gm.adController.showIntAd ();
		}

	}

    //Used when respawning or returning to menu/setup;
    public static void resetScore()
    {
        score = 0;
        ScoreUI.current.UpdateText();
    }

    //Used by options
    public static void resetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("Highscore", GameManager.highScore);
        HighscoreUI.current.UpdateText();

    }

    public static void savePref()
    {

        PlayerPrefs.SetInt("Highscore", GameManager.highScore);

        PlayerPrefs.SetInt("Currency", GameManager.currency);
    }

    public void LoadPrefs()
    {
        highScore = PlayerPrefs.GetInt("Highscore");
        currency = PlayerPrefs.GetInt("Currency");
        //If currency is 0 and no upgrades have been purchased
        if(currency == 0 && PlayerPrefs.GetInt("DamageUpgrades") == 1 && PlayerPrefs.GetInt("DamageUpgrades") == 1)
        {
            currency = 10000;
        }
        
        if (CurrencyUI.current != null)
        {
            CurrencyUI.current.UpdateText();
        }

        if(ScoreUI.current != null)
        {
            ScoreUI.current.UpdateText();
        }

        //Load player selected weapon;
        if (PlayerPrefs.GetString("CurrentWeapon") == "FireBall")
        {
            player.GetComponent<Player>().wep.projectile = GameManager.gm.weaponList[0];
        }
        else if (PlayerPrefs.GetString("CurrentWeapon") == "Beam")
        {
            player.GetComponent<Player>().wep.projectile = GameManager.gm.weaponList[1];
        }
        else
        {
            player.GetComponent<Player>().wep.projectile = GameManager.gm.weaponList[2];
        }


            //If no session key created, create key, but no new session exists on load;
            if (!PlayerPrefs.HasKey("NewSession"))
        {
            //Create key, init to 0;
            PlayerPrefs.SetInt("NewSession", 0);
        }

        



    }

    //For new game initialization;
    public void initPrefs()
    {
        //Highscore persists even when clearing prefs;
        int prevHighscore = PlayerPrefs.GetInt("Highscore");
        PlayerPrefs.DeleteAll();

        //Save the old highscore;
        PlayerPrefs.SetInt("Highscore", prevHighscore);

        //Reset weapon dmg to defaults
        ShootFireBall.damage = 25;
        HomingMissile.damage = 30;
        Beam.damage = 30;

        upgrades.disableAllUpgrades();

        //

        highScore = PlayerPrefs.GetInt("Highscore");
        currency = PlayerPrefs.GetInt("Currency");

        if (currency == 0)
        {
            currency = 0;
        }

        if (CurrencyUI.current != null)
        {
            CurrencyUI.current.UpdateText();
        }

        if (ScoreUI.current != null)
        {
            ScoreUI.current.UpdateText();
        }

        //New game has begun, create new session
        PlayerPrefs.SetInt("NewSession", 1);
    }

    //For game over/quit reset prefs
    public void resetPrefs()
    {
        //Highscore persists even when clearing prefs;
        int prevHighscore = PlayerPrefs.GetInt("Highscore");
        PlayerPrefs.DeleteAll();

        //Save the old highscore;
        PlayerPrefs.SetInt("Highscore", prevHighscore);

        //Reset weapon dmg to defaults
        ShootFireBall.damage = 25;
        HomingMissile.damage = 30;
        Beam.damage = 30;

        upgrades.disableAllUpgrades();

        //Default selected weapon
        player.GetComponent<Player>().wep.projectile = GameManager.gm.weaponList[0]; //fireball

        highScore = PlayerPrefs.GetInt("Highscore");
        currency = PlayerPrefs.GetInt("Currency");

        if (currency == 0)
        {
            currency = 0;
        }

        if (CurrencyUI.current != null)
        {
            CurrencyUI.current.UpdateText();
        }

        if (ScoreUI.current != null)
        {
            ScoreUI.current.UpdateText();
        }

        //Initialize "NewSession" after gameOver, there is no current session;
        PlayerPrefs.SetInt("NewSession", 0);
    }



	void OnApplicationQuit() 
	{
		if (state == gameState.gameOver) 
		{
			//Reset all player preferences and load

			resetPrefs ();

			//Clear screen of enemies and reset score
			clearScreenOfEnemies ();
			respawnPlayer ();
			resetBossFlags ();
			resetScore ();

			//Get new ads
			getNewAds ();

			turnOffAds ();

			StopAllCoroutines ();
		}
	}










}

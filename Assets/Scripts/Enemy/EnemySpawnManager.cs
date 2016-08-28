using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

    public bool spawnEnemies, spawnBoss;

    public static EnemySpawnManager current;
    private Transform myTransform;

    public int maxEnemies = 1; //Equal to the maxEnemies of the current wave
    public int maxBosses = 1;

    public int totalEnemiesSpawned = 0;  //Total enemies spawned
    public int maxEnemiesAllowed = 6;
    public static int currentEnemies = 0;
    public static int currentBosses = 0;

    public GameObject[] enemyTypes;
    public GameObject tutorialEnemy;
    public GameObject[] bossList;

    public GameObject boss;

    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    public Transform[] path;
    public Transform[] path2;
    public Transform[] path3;
    public Transform[] path4;
	public Transform[] path6;
	public Transform[] pathTurret;

    public Transform[] tutorialPath;

    //MirrorBoss//
    public Transform[] path5;

	//Turret Paths
	public Transform[] spotL1, spotL2, spotL3, spotR1, spotR2, spotR3;
	public Transform[][] turretSpots;
	int countTurret;

	//Kamiokaze paths
	public Transform[] spot1, spot2, spot3, spot4, spot5;
	public Transform[][] kamikazepath;
	int objectCount;

    [SerializeField]
    private Transform[] reverse_Path;
    [SerializeField]
    private Transform[] reverse_Path2;
    [SerializeField]
    private Transform[] reverse_Path3;
    [SerializeField]
    private Transform[] reverse_Path4;
	[SerializeField]
	private Transform[] reverse_Path6;


    public float speed = 5f;
    public float drawDis = 1f;
    public int currentPoint = 0;


    GameObject player, originMid;

    [SerializeField]
    private bool occupied; //for IEnumerator

    public static Enemy bossEnemy; //The only active boss;


    [System.Serializable]
    public class Wave
    {
        public int maxEnemies; //max number of enemies
        public GameObject[] enemies; //array of enemies for the wave;
        public float rate; //spawnRate;
        int chosenEnemy;
        public void init()
        {

			int distributionMethod = Random.Range(1, 3);
            Debug.Log("METHOD " + distributionMethod);
           
			if (distributionMethod == 1) {													// Chooses from the the flypass and flyby enemies
				chosenEnemy = Random.Range (0, current.enemyTypes.Length - 2);           // removes the kamikaze enemy type from selection
				Debug.Log (chosenEnemy + " THIS " + current.enemyTypes.Length);

				maxEnemies = Random.Range (10, 20);
				enemies = new GameObject[maxEnemies]; //Array of size max Enemies;
				for (int x = 0; x < enemies.Length; x++) {

					enemies [x] = current.enemyTypes [chosenEnemy];
				}
			
			}else if (distributionMethod == 2){
				chosenEnemy = Random.Range(0, current.enemyTypes.Length - 1);
				Debug.Log(chosenEnemy + " THIS " + current.enemyTypes.Length);			

				maxEnemies = Random.Range (10, 20);

				enemies = new GameObject[maxEnemies]; //Array of size max Enemies;

				if(chosenEnemy == 3)														// turret type will only spawn once in groups of 6 if chosen
				{
					for (int x = 0; x < 6; x++)
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}

					chosenEnemy = Random.Range(0, current.enemyTypes.Length - 2);

					for (int x = 6; x < enemies.Length; x++)							// rest of wave is filled with other types
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}
				}
				else
				{
					for (int x = 0; x < enemies.Length; x++)
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}
				}
				
			} else if (distributionMethod == 3) {										// includes the kamikaze type
				chosenEnemy = Random.Range(0, current.enemyTypes.Length);
				Debug.Log(chosenEnemy + " THIS " + current.enemyTypes.Length);			

				maxEnemies = Random.Range (11, 20);
				
				enemies = new GameObject[maxEnemies]; //Array of size max Enemies;

				if(chosenEnemy == 4)														// kamikaze type will only spawn once in groups of 5 if chosen
				{
					for (int x = 0; x < 5; x++)
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}

					chosenEnemy = Random.Range(0, current.enemyTypes.Length - 2);

					for (int x = 5; x < enemies.Length; x++)							// rest of wave is filled with other types
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}
				}
				else if(chosenEnemy == 3)														// turret type will only spawn once in groups of 6 if chosen
				{
					for (int x = 0; x < 6; x++)
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}

					chosenEnemy = Random.Range(0, current.enemyTypes.Length - 2);

					for (int x = 6; x < enemies.Length; x++)							// rest of wave is filled with other types
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}
				}
				else
				{
					for (int x = 0; x < enemies.Length; x++)
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}
				}
			}
            else
            {
                
                Debug.Log(chosenEnemy + " THIS " + current.enemyTypes.Length);

                maxEnemies = Random.Range(10, 20);
                enemies = new GameObject[maxEnemies]; //Array of size max Enemies;
                for (int x = 0; x < enemies.Length; x++)
                {
                    chosenEnemy = Random.Range(0, current.enemyTypes.Length);
                    enemies[x] = current.enemyTypes[chosenEnemy];
                }
            }
        }
        
    }

    public Wave[] waves;
    [SerializeField]
    private int currentWave = 0;

    void Awake()
    {
        current = this;
        spawnEnemies = true;

        spawnBoss = false;
        myTransform = transform;

        waves = new Wave[1];


        //Create copies of normal path arrays;
        reverse_Path = (Transform[]) path.Clone();
        reverse_Path2 = (Transform[])path2.Clone();
        reverse_Path3 = (Transform[])path3.Clone();
        reverse_Path4 = (Transform[])path4.Clone();
		reverse_Path6 = (Transform[])path6.Clone();
        //Create reverse copies of the path arrays;
        System.Array.Reverse(reverse_Path);
        System.Array.Reverse(reverse_Path2);
        System.Array.Reverse(reverse_Path3);
        System.Array.Reverse(reverse_Path4);
		System.Array.Reverse(reverse_Path6);

		objectCount = 0;
		countTurret = 0;


    }


    // Use this for initialization
    void Start () {
        
        player = GameObject.FindGameObjectWithTag("Player");
        originMid = GameObject.FindGameObjectWithTag("Origin");

        if (player == null)
        {
            print("Player object not found");
        }

        if (originMid == null)
        {
            print("Origin object not found");
        }


        for (int x = 0; x < waves.Length; x++)
        {
            waves[x] = new Wave();
            waves[x].init();
        }

		kamikazepath = new Transform[5][];

		kamikazepath [0] = spot1;
		kamikazepath [1] = spot2;
		kamikazepath [2] = spot3;
		kamikazepath [3] = spot4;
		kamikazepath [4] = spot5;

		turretSpots = new Transform[6][];
		turretSpots [0] = spotL1;
		turretSpots [1] = spotR1;
		turretSpots [2] = spotL2;
		turretSpots [3] = spotR2;
		turretSpots [4] = spotL3;
		turretSpots [5] = spotR3;

		objectCount = 0;
		countTurret = 0;

        occupied = false;

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
      
        //Change position/velocity of origin using the same process that moves the player, not using rigidbody
        //transform.Translate(originMid.GetComponent<Origin>().getVelocity() * Time.deltaTime);

        //Since position is not being tracked by raycasting, like the Player is, this line is needed to maintain the Y-position;
        myTransform.position = new Vector3(originMid.transform.position.x + 30, originMid.transform.position.y, 0);

        if (GameManager.gm.state == GameManager.gameState.setup)
        {
            if (!current.occupied)
            {
                StartCoroutine(startFirstWave());
            }
        }


        if (GameManager.gm.state == GameManager.gameState.menu)
        {
            totalEnemiesSpawned = 0;
            spawnEnemies = true;
        }

  



    }

    void Spawn()
    {
        if(currentEnemies == maxEnemiesAllowed)
        {
            StartCoroutine(tooManyEnemies());
        }

        if(spawnEnemies && (GameManager.gm.state == GameManager.gameState.tutorial_2 || GameManager.gm.state == GameManager.gameState.tutorial_3))
        {
            if(currentEnemies <= 0)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);

                GameObject temp = EnemyObjectPool.current.getPooledObject(tutorialEnemy);
                temp.transform.position = spawnPoints[spawnPointIndex].position;
                temp.transform.rotation = spawnPoints[spawnPointIndex].rotation;

                currentEnemies++;
                totalEnemiesSpawned++;

                if (temp == null) return;

                if(temp.tag == "Tutorial_Enemy")
                {
                    temp.GetComponent<EnemyAI>().assignPath(tutorialPath);
                }

                temp.SetActive(true);
            }


        }

        if (spawnEnemies && GameManager.gm.state == GameManager.gameState.normalPlay)
        {
            
           
            if (totalEnemiesSpawned < waves[currentWave].maxEnemies && (currentWave < waves.Length)) //if total enemies spawned is less than the amount of total enemies in the wave, continue spawning wave
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                //GameObject temp = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
                GameObject temp = EnemyObjectPool.current.getPooledObject(waves[currentWave].enemies[totalEnemiesSpawned]); //iterate through enemy types of the wave
                temp.transform.position = spawnPoints[spawnPointIndex].position;
                temp.transform.rotation = spawnPoints[spawnPointIndex].rotation;
                if (temp == null) return;

				if (temp.tag == "Fly_By") {
					if (spawnPointIndex == 0) {
						temp.GetComponent<EnemyAI> ().assignPath (path);
					} else {
						temp.GetComponent<EnemyAI> ().assignPath (reverse_Path);
					}
				
				}else if (temp.tag == "Fly_By2") {
					if (spawnPointIndex == 0) {
						temp.GetComponent<EnemyAI> ().assignPath (reverse_Path6);
					} else {
						temp.GetComponent<EnemyAI> ().assignPath (path6);
				
					}
				} else if (temp.tag == "Fly_Pass") {
					if (spawnPointIndex == 0) {
						temp.GetComponent<EnemyAI> ().assignPath (path4);
					} else {
						temp.GetComponent<EnemyAI> ().assignPath (reverse_Path4);
					}
				} else if (temp.tag == "Turret") {
					if (countTurret == 6) {
						countTurret = 0;
					}

					temp.GetComponent<EnemyAI> ().assignPath (turretSpots [countTurret]);
					countTurret++;
				

					print ("Turret Count: " + countTurret);

				} else if (temp.tag == "Kamikaze") {
					if (objectCount == 5) {
						objectCount = 0;
                    }
                    temp.GetComponent<EnemyAI>().assignPath(kamikazepath[objectCount]);
                    objectCount++;


                    print (objectCount);
				}

                
                temp.SetActive(true);

                /*
                if (temp.tag != "Boss" && !temp.name.Contains("Boss_Enemy2"))
                {
                    temp.transform.parent = transform;

                }
                */
                currentEnemies++;
                totalEnemiesSpawned++;

            }
            else
            {
                spawnEnemies = false;
            }
        }

        if(currentWave < waves.Length)
        {
            if (currentEnemies == 0 && totalEnemiesSpawned == waves[currentWave].maxEnemies) //all enemies of current wave destroyed
            {
                if (!occupied)
                {
                    StartCoroutine(startNextWave());
                }
            }
        }
       


        if (spawnBoss && GameManager.gm.state == GameManager.gameState.prepareForBoss)
        {
            if (!occupied && spawnBoss) 
            {
                StartCoroutine(prepareForBoss());
            }
            
        } 

    }

    IEnumerator tooManyEnemies()
    {

        while(currentEnemies == maxEnemiesAllowed)
        {
            spawnEnemies = false;
            yield return null;
        }

        spawnEnemies = true;

    }



    IEnumerator prepareForBoss()
    {
        occupied = true;
        print("INCOMING BOSS");
        BossUI.current.bossGuiAnim.SetBool("Normal", false);
        BossUI.current.bossGuiAnim.SetBool("Warning", true);

        int bossSelection = Random.Range(0, bossList.Length);
        boss = bossList[bossSelection];

        yield return new WaitForSeconds(3);
        if (currentBosses < maxBosses)
        {
            //int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            int spawnPointIndex = Random.Range(0, 1);

            GameObject temp;

            if (boss.name.Contains("Boss_Enemy3"))
            {
                //temp = Instantiate(boss, new Vector3(spawnPoints[spawnPointIndex].position.x, spawnPoints[spawnPointIndex].position.y, boss.transform.position.z), spawnPoints[spawnPointIndex].rotation) as GameObject;
                temp = EnemyObjectPool.current.getPooledObject(boss); //iterate through enemy types of the wave
                temp.transform.position = spawnPoints[spawnPointIndex].position;
                temp.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            }
            else if (boss.name.Contains("Boss_Enemy1"))
            {
                //temp = Instantiate(boss, new Vector3(spawnPoints[spawnPointIndex].position.x, spawnPoints[spawnPointIndex].position.y, boss.transform.position.z), spawnPoints[spawnPointIndex].rotation) as GameObject;
                temp = EnemyObjectPool.current.getPooledObject(boss); //iterate through enemy types of the wave
                temp.transform.position = spawnPoints[spawnPointIndex].position;
                temp.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            }
            else if (boss.name.Contains("Boss_Enemy2"))
            {
                //temp = Instantiate(boss, new Vector3(spawnPoints[spawnPointIndex].position.x, spawnPoints[spawnPointIndex].position.y, boss.transform.position.z), spawnPoints[spawnPointIndex].rotation) as GameObject;
                temp = EnemyObjectPool.current.getPooledObject(boss); //iterate through enemy types of the wave
                temp.transform.position = spawnPoints[spawnPointIndex].position;
                temp.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            }
            else
            {
                temp = Instantiate(boss, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
            }

            /*
            GameObject temp = EnemyObjectPool.current.getPooledObject(boss);
            temp.transform.position = spawnPoints[spawnPointIndex].position;
            temp.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            */

            
            temp.GetComponent<EnemyAI>().assignPath(path2);
            if (temp.name.Contains("Boss_Enemy3"))
            {
                temp.GetComponent<EnemyAI>().assignPath(path3);
            }
            temp.SetActive(true);


            if (temp.name.Contains("Boss_Enemy2") || temp.name.Contains("Boss_Enemy3"))
            {
                temp.transform.parent = transform;

            }

            bossEnemy = temp.GetComponent<Enemy>();
            BossUI.current.boss = temp.GetComponent<Enemy>();
            GameManager.gm.state = GameManager.gameState.bossFight;
            BossUI.current.bossGuiAnim.SetBool("Warning", false);
            BossUI.current.bossGuiAnim.SetBool("ShowBossHealth", true);
            currentBosses++;
            totalEnemiesSpawned++;
        }
        yield return new WaitForSeconds(2f);
        BossUI.current.bossGuiAnim.enabled = false;
        BossUI.current.bossGuiAnim.SetBool("ShowBossHealth", false);
        BossUI.current.bossGuiAnim.SetBool("BossFightReady", true);
        spawnBoss = occupied = false;
    }

    IEnumerator startFirstWave()
    {
        occupied = true;
        currentWave = 0;
        currentEnemies = 0;
        totalEnemiesSpawned = 0;
        spawnEnemies = true;
        occupied = false;

        yield return new WaitForSeconds(0f);
    }
    IEnumerator startNextWave()
    {
        occupied = true;
   
        currentWave++;
        if (currentWave >= waves.Length) //It is the last wave and all enemies have been destroyed
        {
            GameManager.gm.state = GameManager.gameState.prepareForBoss;
            EnemySpawnManager.current.spawnBoss = true;
        }
        else
        {
            currentEnemies = 0;
            totalEnemiesSpawned = 0;
            spawnEnemies = true;
            
        }
        yield return new WaitForSeconds(0f);
        occupied = false;

    }

    public void reinit() //should be called after every boss fight by the GameManager during state transitions
    {
        int waveCount = Random.Range(1,2);
        waves = new Wave[waveCount];
        for (int x = 0; x < waves.Length; x++)
        {
            waves[x] = new Wave();
            waves[x].init();
        }
    }

    void OnDrawGizmos()
    {
        for(int x = 0; x < path.Length; x++)
        {
            if(path[x] != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(path[x].position, drawDis);
            }
            
        }

        for (int x = 0; x < path2.Length; x++)
        {

            if (path2[x] != null)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawSphere(path2[x].position, drawDis);
            }
        }

        for (int x = 0; x < path3.Length; x++)
        {

            if (path3[x] != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(path3[x].position, drawDis);
            }
        }

        for (int x = 0; x < path4.Length; x++)
        {

            if (path4[x] != null)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(path4[x].position, drawDis);
            }
        }
        
		for (int x = 0; x < path5.Length; x++)
        {

            if (path5[x] != null)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawSphere(path5[x].position, drawDis);
            }
        }

		for (int x = 0; x < path6.Length; x++)
		{

			if (path6[x] != null)
			{
				Gizmos.color = Color.green;
				Gizmos.DrawSphere(path6[x].position, drawDis);
			}
		}

		for (int x = 0; x < pathTurret.Length; x++)
		{

			if (path6[x] != null)
			{
				Gizmos.color = Color.gray;
				Gizmos.DrawSphere(pathTurret[x].position, drawDis);
			}
		}

        for (int x = 0; x < tutorialPath.Length; x++)
        {

            if (path6[x] != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(tutorialPath[x].position, drawDis);
            }
        }


        for (int x = 0; x < spawnPoints.Length; x++)
        {

            if (spawnPoints[x] != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(spawnPoints[x].position, drawDis);
            }
        }
        
    }


}

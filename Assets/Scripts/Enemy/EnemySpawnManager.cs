﻿using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

    public static EnemySpawnManager current;
    private Transform myTransform;

    public int maxEnemies = 1; //Equal to the maxEnemies of the current wave
    public int maxBosses = 1;
	int objectCount = 0;

    public int totalEnemiesSpawned = 0;
    public int maxEnemiesAllowed = 6;
    public static int currentEnemies = 0;
    public static int currentBosses = 0;

    public GameObject[] enemyTypes;
    public GameObject[] bossList;

    public GameObject boss;

    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    public Transform[] path;
    public Transform[] path2;
    public Transform[] path3;
    public Transform[] path4;
	public Transform[] path6;

    //MirrorBoss//
    public Transform[] path5;

	//Kamiokaze paths
	public Transform[] spot1, spot2, spot3, spot4, spot5;
	public Transform[][] kamikazepath;

    [SerializeField]
    private Transform[] reverse_Path;
    [SerializeField]
    private Transform[] reverse_Path2;
    [SerializeField]
    private Transform[] reverse_Path3;
    [SerializeField]
    private Transform[] reverse_Path4;


    public float speed = 5f;
    public float drawDis = 1f;
    public int currentPoint = 0;


    GameObject player, originMid;

    public bool spawnEnemies, spawnBoss;

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

			int distributionMethod = 2; //Random.Range(1, 3);
            Debug.Log("METHOD " + distributionMethod);
           
            if(distributionMethod == 1)													// Chooses from the the flypass and flyby enemies
            {
				chosenEnemy = Random.Range(0, current.enemyTypes.Length - 1);           // removes the kamikaze enemy type from selection
                Debug.Log(chosenEnemy + " THIS " + current.enemyTypes.Length);

                maxEnemies = Random.Range(10, 20);
                enemies = new GameObject[maxEnemies]; //Array of size max Enemies;
                for (int x = 0; x < enemies.Length; x++)
                {

                    enemies[x] = current.enemyTypes[chosenEnemy];
                }
			} else if (distributionMethod == 2) {										// includes the kamikaze type
				chosenEnemy = 2; //Random.Range(0, current.enemyTypes.Length);
				Debug.Log(chosenEnemy + " THIS " + current.enemyTypes.Length);			

				maxEnemies = Random.Range (10, 20);
				
				enemies = new GameObject[maxEnemies]; //Array of size max Enemies;

				if(chosenEnemy == 2)														// kamikaze type will only spawn once in groups of 5 if chosen
				{
					for (int x = 0; x < 5; x++)
					{
						enemies[x] = current.enemyTypes[chosenEnemy];
					}

					chosenEnemy = Random.Range(0, current.enemyTypes.Length - 1);

					for (int x = 5; x < enemies.Length; x++)							// rest of wave is filled with other types
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
        //Create reverse copies of the path arrays;
        System.Array.Reverse(reverse_Path);
        System.Array.Reverse(reverse_Path2);
        System.Array.Reverse(reverse_Path3);
        System.Array.Reverse(reverse_Path4);



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

        
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
      
        //Change position/velocity of origin using the same process that moves the player, not using rigidbody
        //transform.Translate(originMid.GetComponent<Origin>().getVelocity() * Time.deltaTime);

        //Since position is not being tracked by raycasting, like the Player is, this line is needed to maintain the Y-position;
        myTransform.position = new Vector3(originMid.transform.position.x + 30, originMid.transform.position.y, 0);



    }

    void Spawn()
    {
        if(currentEnemies == maxEnemiesAllowed)
        {
            StartCoroutine(tooManyEnemies());
        }

        if (GameManager.gm.state == GameManager.gameState.setup)
        {
            if (!occupied)
            {
                StartCoroutine(startFirstWave());
            }
        }

        if (spawnEnemies && GameManager.gm.state == GameManager.gameState.normalPlay)
        {
            
           
            if (totalEnemiesSpawned < waves[currentWave].maxEnemies && (currentWave < waves.Length)) //replace with currentWave
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
                    
				} else if (temp.tag == "Fly_Pass") {
					if (spawnPointIndex == 0) {
						temp.GetComponent<EnemyAI> ().assignPath (path4);
					} else {
						temp.GetComponent<EnemyAI> ().assignPath (reverse_Path4);
					}
                    
				} else if (temp.tag == "Kamikaze") {
					if (objectCount == 5) {
						objectCount = 0;
					} else {
						temp.GetComponent<EnemyAI> ().assignPath (kamikazepath [objectCount]);
						objectCount++;
					}
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
        
        currentWave = 0;
        occupied = true;
        yield return new WaitForSeconds(0f);
        totalEnemiesSpawned = 0;
        spawnEnemies = true;
        occupied = false;
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

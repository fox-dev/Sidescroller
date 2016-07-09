using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

    public static EnemySpawnManager current;
    private Transform myTransform;

    public int maxEnemies = 1; //Equal to the maxEnemies of the current wave
    public int maxBosses = 1;

    public int totalEnemiesSpawned = 0;
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

            int distributionMethod = Random.Range(1, 3);
            Debug.Log("METHOD " + distributionMethod);
           
            if(distributionMethod == 1)
            {
                chosenEnemy = Random.Range(0, current.enemyTypes.Length);
                Debug.Log(chosenEnemy + " THIS " + current.enemyTypes.Length);

                maxEnemies = Random.Range(10, 20);
                enemies = new GameObject[maxEnemies]; //Array of size max Enemies;
                for (int x = 0; x < enemies.Length; x++)
                {

                    enemies[x] = current.enemyTypes[chosenEnemy];
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

                if(temp.tag == "Fly_By")
                {
                    temp.GetComponent<EnemyAI>().assignPath(path);
                }
                else if(temp.tag == "Fly_Pass")
                {
                    temp.GetComponent<EnemyAI>().assignPath(path4);
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
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);

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
        int waveCount = Random.Range(1, 2);
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

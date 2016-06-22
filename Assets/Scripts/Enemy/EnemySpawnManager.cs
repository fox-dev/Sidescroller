using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

    public static EnemySpawnManager current;

    public int maxEnemies = 1;
    public int maxBosses = 1;

    public int totalEnemiesSpawned = 0;
    public static int currentEnemies = 0;
    public static int currentBosses = 0;

    public GameObject enemy;
    public GameObject boss;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    public Transform[] path;
    public Transform[] path2;

    public float speed = 5f;
    public float drawDis = 1f;
    public int currentPoint = 0;


    GameObject player, originMid;

    public bool spawnEnemies, spawnBoss;

    private bool occupied; //for IEnumerator

    public static Enemy bossEnemy;

    void Awake()
    {
        current = this;
        spawnEnemies = true;
        spawnBoss = false;
    }


    // Use this for initialization
    void Start () {

        player = GameObject.FindGameObjectWithTag("Player");
        originMid = GameObject.FindGameObjectWithTag("OriginMid");

        if (player == null)
        {
            print("Player object not found");
        }

        if (originMid == null)
        {
            print("Origin object not found");
        }

        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {

        //Change position/velocity of origin using the same process that moves the player, not using rigidbody
        //transform.Translate(originMid.GetComponent<Origin>().getVelocity() * Time.deltaTime);

        //Since position is not being tracked by raycasting, like the Player is, this line is needed to maintain the Y-position;
        transform.position = new Vector3(originMid.transform.position.x, originMid.transform.position.y, 0);



    }

    void Spawn()
    {

        if (spawnEnemies && GameManager.gm.state == GameManager.gameState.normalPlay)
        {
            if (currentEnemies < maxEnemies)
            {
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                //GameObject temp = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
                GameObject temp = EnemyObjectPool.current.getPooledObject(enemy);
                temp.transform.position = spawnPoints[spawnPointIndex].position;
                temp.transform.rotation = spawnPoints[spawnPointIndex].rotation;
                if (temp == null) return;

                temp.GetComponent<EnemyAI>().assignPath(path);
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
        }

        if(spawnBoss && GameManager.gm.state != GameManager.gameState.bossFight)
        {
            if (!occupied && spawnBoss)
            {
                StartCoroutine(prepareForBoss());
            }
            
        }

        if (GameManager.gm.state == GameManager.gameState.waiting)
        {
            if (!occupied)
            {
                StartCoroutine(startNextWave());
            }
        }

    }



    IEnumerator prepareForBoss()
    {
        occupied = true;
        print("INCOMING BOSS");
        BossUI.current.bossGuiAnim.SetBool("Normal", false);
        BossUI.current.bossGuiAnim.SetBool("Warning", true);

   
        yield return new WaitForSeconds(3);
        if (currentBosses < maxBosses)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject temp = Instantiate(boss, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;

            /*
            GameObject temp = EnemyObjectPool.current.getPooledObject(boss);
            temp.transform.position = spawnPoints[spawnPointIndex].position;
            temp.transform.rotation = spawnPoints[spawnPointIndex].rotation;
            */

            temp.GetComponent<EnemyAI>().assignPath(path2);
            temp.SetActive(true);


            if (temp.name.Contains("Boss_Enemy2"))
            {
                temp.transform.parent = transform;

            }
       
            BossUI.current.boss = temp.GetComponent<Enemy>();
            GameManager.gm.state = GameManager.gameState.bossFight;
            BossUI.current.bossGuiAnim.SetBool("Warning", false);
            BossUI.current.bossGuiAnim.SetBool("ShowBossHealth", true);
            currentBosses++;
            totalEnemiesSpawned++;
        }
        spawnBoss = occupied = false;
    }

    IEnumerator startNextWave()
    {
        occupied = true;
        yield return new WaitForSeconds(5f);
        totalEnemiesSpawned = 0;
        spawnEnemies = true;
        GameManager.gm.state = GameManager.gameState.normalPlay;
        occupied = false;
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
    }
}

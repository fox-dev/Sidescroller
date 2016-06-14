using UnityEngine;
using System.Collections;

public class EnemySpawnManager : MonoBehaviour {

    public int maxEnemies = 1;
    public int maxBosses = 1;
    int currentEnemies = 0;
    int currentBosses = 0;

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
        if(currentEnemies < maxEnemies)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject temp = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
            temp.GetComponent<EnemyAI>().assignPath(path);

            if (temp.tag != "Boss" && !temp.name.Contains("Boss_Enemy2"))
            {
                temp.transform.parent = transform;

            }
            currentEnemies++;
   
        }

        if (currentBosses < maxBosses)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject temp = Instantiate(boss, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
            temp.GetComponent<EnemyAI>().assignPath(path2);
            
           
            if (temp.name.Contains("Boss_Enemy2"))
            {
                temp.transform.parent = transform;
                
            }
            currentBosses++;
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
    }
}

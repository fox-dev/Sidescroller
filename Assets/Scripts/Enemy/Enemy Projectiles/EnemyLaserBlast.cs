using UnityEngine;
using System.Collections;

public class EnemyLaserBlast : MonoBehaviour
{
    GameObject player;
    GameObject crosshair;

    public GameObject particle;

    public float range = 10f;
    public int damage = 50;
    public float maxWidth = 7f;
    public float startWidth = 0.025f;
    public float growingWidth = 0.025f;

    public float startWidth2 = 7f;
    public float growingWidth2 = 7f;

    Ray shootRay;
    RaycastHit shootHit;

    public LayerMask shootableMask;
    LineRenderer gunLine;

    [SerializeField]
    private bool occupied = false;
    private bool initialFire = false;
    private bool grow = false;
    private bool shrink = false;
    private bool damageEnabled = false;

    Quaternion init;


    Quaternion lookRotation;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    void OnEnable()
    {
        init = transform.rotation;

        growingWidth = 0.025f;
        growingWidth2 = 3f;
        initialFire = false;
        grow = false;
        shrink = false;
        occupied = false;
        damageEnabled = false;

        GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);


        
        ////////////////

        gunLine = GetComponent<LineRenderer>();

        lookRotation = Quaternion.LookRotation(-transform.right) * Quaternion.Euler(90, 0, 0);
        transform.rotation = lookRotation;

        if (EnemySpawnManager.bossEnemy == null)
        {
            //shootRay.origin = transform.position;
            //shootRay.direction = (shootRay.origin - transform.right * 20);

            //gunLine.SetPosition(0, transform.position);
        }
        else
        {
            print("WE'RE HERE");
            shootRay.origin = EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position;
            shootRay.direction = (transform.forward);

            gunLine.SetPosition(0, EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position);
        }
        print(transform.forward);

       
        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);

        



    }

    // Update is called once per frame
    void Update()
    {


        transform.rotation = EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.rotation;
        lookRotation = Quaternion.LookRotation(-transform.right) * Quaternion.Euler(90, 0, 0);
        transform.rotation = lookRotation;
        shootRay.origin = EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position;
        shootRay.direction = (transform.forward);
        transform.position = EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position;
        gunLine.SetPosition(0, EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position);
        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);



        print(shootRay.origin + shootRay.direction * range);

        if (growingWidth <= maxWidth - 1f && !grow && !initialFire)
        {
            growingWidth = Mathf.Lerp(growingWidth, 0.5f, Time.deltaTime * 5f);
            gunLine.SetWidth(growingWidth, growingWidth);
            if (!occupied && (growingWidth <= maxWidth - 1f))
            {
                StartCoroutine(prepareBlast());
            }
        }

        if (growingWidth <= maxWidth - 1f && grow)
        {
            growingWidth = Mathf.Lerp(growingWidth, maxWidth, Time.deltaTime * 1.5f);
            gunLine.SetWidth(growingWidth, growingWidth);
            if (growingWidth >= maxWidth - 1f)
            {
                shrink = true;
            }
        }

        if (shrink)
        {
            grow = false;
            growingWidth = Mathf.Lerp(growingWidth, 0, Time.deltaTime * 10f);
            gunLine.SetWidth(growingWidth, growingWidth);
        }
        

    }

    IEnumerator prepareBlast()
    {
        occupied = true;
        yield return new WaitForSeconds(2f);
        damageEnabled = true;
        grow = true;
        initialFire = true;
        occupied = false;

    }


}

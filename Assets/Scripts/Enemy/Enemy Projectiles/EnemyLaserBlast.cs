using UnityEngine;
using System.Collections;

public class EnemyLaserBlast : MonoBehaviour
{
    GameObject player;
   

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



    private Quaternion lookRotation;
    private Quaternion currentRotation;
    private Transform myTransform;
    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myTransform = transform;
        currentRotation = new Quaternion();
        
    }

    void OnEnable()
    {
        

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
            shootRay.origin = EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position;
            shootRay.direction = (transform.forward);

            gunLine.SetPosition(0, EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position);
        }
        

       
        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);





        AudioManager.current.playLASERCHARGE_ENEMY();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (EnemySpawnManager.bossEnemy != null)
        {
            myTransform.rotation = EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.rotation;
            lookRotation = Quaternion.LookRotation(myTransform.right) * Quaternion.Euler(90, 0, 0); //Multiply quaternions to do addition: myTransform.right + 90
            myTransform.rotation = lookRotation;
            shootRay.origin = EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position;
            shootRay.direction = (myTransform.forward);
            transform.position = EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position;
            gunLine.SetPosition(0, EnemySpawnManager.bossEnemy.GetComponentInChildren<Enemy_Weapon>().transform.position);
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
        
        

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
            CameraScript.camera.ShakeCam(0.3f, 0.5f);
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
            damageEnabled = false;
            growingWidth = Mathf.Lerp(growingWidth, 0, Time.deltaTime * 10f);
            gunLine.SetWidth(growingWidth, growingWidth);
        }

        if (Physics.SphereCast(shootRay, 5f, out shootHit, range, shootableMask) && damageEnabled)
        {
            if (shootHit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Player player = shootHit.collider.GetComponent<Player>();
                if (player != null)
                {
                    player.DamagePlayer(damage);
                }
                else
                {
                    Debug.Log("Player object does not exist");
                }
            }
        }

    }

    IEnumerator prepareBlast()
    {
        occupied = true;
        yield return new WaitForSeconds(2f);

        AudioManager.current.playLASERBLAST ();
        damageEnabled = true;
        grow = true;
        initialFire = true;
        occupied = false;

    }


}

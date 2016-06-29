using UnityEngine;
using System.Collections;

public class LaserBlast : MonoBehaviour
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




    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    void OnEnable()
    {
        //growingWidth2 = 5f;
        //growingWidth = 0.025f;

        growingWidth = 0.025f;
        growingWidth2 = 3f;
        initialFire = false;
        grow = false;
        shrink = false;
        occupied = false;
        damageEnabled = false;

        GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);


        Vector3 pos_at_z_0 = crosshair.transform.position;
        Vector2 point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);
        ////////////////

        gunLine = GetComponent<LineRenderer>();

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        shootRay.origin = transform.position;
        shootRay.direction = (point - currentPos);

        gunLine.SetPosition(0, transform.position);

        Vector3 direction = (point - currentPos).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);

        //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);

        if (Physics.SphereCast(shootRay, 0.5f, out shootHit, range, shootableMask) && damageEnabled)
        {
            //hit an enemy goes here
            //gunLine.SetPosition(1, shootHit.point);
           
            GameObject hitEffect = ObjectPool.current.getPooledObject(particle);

            if (hitEffect == null) return;
            hitEffect.transform.position = shootHit.point;
            hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, shootHit.normal);

            hitEffect.SetActive(true);
            //Instantiate(Resources.Load("HitParticles"), shootHit.point, Quaternion.FromToRotation(Vector3.up, shootHit.normal));
            if (shootHit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Enemy enemy = shootHit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(damage);
                    Debug.Log("We hit " + shootHit.collider.name + " and did " + damage + " damage.");
                }
            }


            Debug.Log("We hit " + shootHit.collider.name + " and did" + damage + " damage.");

        }
        else
        {
            //gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }



    }

    // Update is called once per frame
    void Update()
    {
        //To keep the particleShot effect on the player
        transform.position = player.transform.position;

        
        //This block of code has laser continuously track and move line-renderer//
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float z_plane_of_2d_game = 0;
        Vector3 pos_at_z_0 = ray.origin + ray.direction * (z_plane_of_2d_game - ray.origin.z) / ray.direction.z;
        Vector2 point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);
        Vector2 currentPos = new Vector2(player.transform.position.x, player.transform.position.y);
        shootRay.origin = player.transform.position;
        shootRay.direction = (point - currentPos);
        //////////////////////////////////////////////////////////////////////
        
        if (Physics.SphereCast(shootRay, 12.5f, out shootHit, range, shootableMask) && damageEnabled)
        {
            //hit an enemy goes here
            //gunLine.SetPosition(1, shootHit.point);
            Enemy enemy = shootHit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.DamageEnemy(damage);
                Debug.Log("We hit " + shootHit.collider.name + " and did " + damage + " damage.");
                
            }

            GameObject hitEffect = ObjectPool.current.getPooledObject(particle);
            if (hitEffect == null) return;
            hitEffect.transform.position = shootHit.point;
            hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, shootHit.normal);

            hitEffect.SetActive(true);


        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }


        gunLine.SetPosition(0, player.transform.position);
        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);

       
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
            if(growingWidth >= maxWidth - 1f)
            {
                shrink = true;
            }
        }

        if (shrink)
        {
            grow = false;
            //print("BIG");
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

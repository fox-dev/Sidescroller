using UnityEngine;
using System.Collections;

public class Beam : MonoBehaviour {
    GameObject player;
    GameObject crosshair;

    public GameObject particle;

    public float range = 10f;

    public static int damage = 30;
    [SerializeField]
    private int viewDamage = damage;

    public float maxWidth = 7f;
    public float startWidth = 0.025f;
    public float growingWidth = 0.025f;

    public float startWidth2 = 7f;
    public float growingWidth2 = 7f;

    Ray shootRay;
    RaycastHit shootHit;
    
    public LayerMask shootableMask;
    LineRenderer gunLine;

    //UpgradedBeamPurchased for piercing
    RaycastHit[] shootHitPath;



	// Use this for initialization
	void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
    }

    void OnEnable()
    {
        viewDamage = damage;
        //growingWidth2 = 5f;
        //growingWidth = 0.025f;

        growingWidth = 0.025f;
        growingWidth2 = 3f;

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

        
        ///////////////Basic functions, no upgrades, single target/////////////////
        if(!GameManager.gm.upgrades.pierceBeam)
        {
            if (Physics.SphereCast(shootRay, 0.5f, out shootHit, range, shootableMask))
            {
                //hit an enemy goes here
                gunLine.SetPosition(1, shootHit.point);

                //Play hit effect
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

                    }
                }
                //Debug.Log("We hit " + shootHit.collider.name + " and did" + damage + " damage.");
            }
            else
            {
                gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
            }
        }
        //////////////////////////////////////////////////////////////////////////////
        /////////////Upgrades purchased, piercing/////////////////////////////////////
        else
        {
            shootHitPath = Physics.SphereCastAll(shootRay, 0.5f, range, shootableMask);

            foreach (RaycastHit hit in shootHitPath)
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.DamageEnemy(damage);

                    }

                    GameObject hitEffect = ObjectPool.current.getPooledObject(particle);
                    if (hitEffect == null) return;
                    hitEffect.transform.position = hit.point;
                    hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    hitEffect.SetActive(true);
                }

            }
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }



    }
	
	// Update is called once per frame
	void Update () {
        //To keep the particleShot effect on the player
        transform.position = player.transform.position;

        /*
        //This block of code has laser continuously track and move line-renderer//
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float z_plane_of_2d_game = 0;
        Vector3 pos_at_z_0 = ray.origin + ray.direction * (z_plane_of_2d_game - ray.origin.z) / ray.direction.z;
        Vector2 point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);
        Vector2 currentPos = new Vector2(player.transform.position.x, player.transform.position.y);
        shootRay.origin = player.transform.position;
        shootRay.direction = (point - currentPos);
        //////////////////////////////////////////////////////////////////////
        */
        /*
        if (Physics.SphereCast(shootRay, 0.5f, out shootHit, range, shootableMask))
        {
            //hit an enemy goes here
            gunLine.SetPosition(1, shootHit.point);
         

        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
        */

        gunLine.SetPosition(0, player.transform.position);
       // gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);

        ////

        /*
        if (growingWidth <= maxWidth - 1f)
        {
            growingWidth = Mathf.Lerp(growingWidth, maxWidth, Time.deltaTime * 10f);
            gunLine.SetWidth(startWidth, growingWidth);
        }
        else if(growingWidth >= maxWidth - 1f)
        {
            growingWidth2 = Mathf.Lerp(growingWidth2, 0, Time.deltaTime * 10f);
            gunLine.SetWidth(startWidth, growingWidth2);
        }
        */

        if (growingWidth <= maxWidth - 1f)
        {
            growingWidth = Mathf.Lerp(growingWidth, maxWidth, Time.deltaTime * 10f);
            gunLine.SetWidth(growingWidth, growingWidth);
        }
        else if (growingWidth >= maxWidth - 1f)
        {
            growingWidth2 = Mathf.Lerp(growingWidth2, 0, Time.deltaTime * 10f);
            gunLine.SetWidth(growingWidth2, growingWidth2);
        }

    }


}

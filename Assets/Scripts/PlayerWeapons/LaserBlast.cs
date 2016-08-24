using UnityEngine;
using System.Collections;

public class LaserBlast : MonoBehaviour
{
    GameObject player;
    private PlayerWeapon playerWeapon; //to tell the weapon to stop decrementing super gauge and start recharging;
    GameObject crosshair;

    public GameObject particle; //hit particle

    private Transform myTransform;

    public float range = 10f;
    public int damage = 50;
    public float maxWidth = 7f;
    public float startWidth = 0.025f;
    public float growingWidth = 0.025f;

    public float startWidth2 = 7f;
    public float growingWidth2 = 7f;

    Ray shootRay;
    RaycastHit shootHit;
    RaycastHit[] shootHitPath;

    public LayerMask shootableMask;
    LineRenderer gunLine;

    [SerializeField]
    private bool occupied = false;
    private bool initialFire = false;
    private bool grow = false;
    private bool shrink = false;
    private bool damageEnabled = false;

    ParticleSystem.ShapeModule shapeModule;
    ParticleSystem ps; 




    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerWeapon = player.GetComponent<Player>().wep;
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        shapeModule = GetComponent<ParticleSystem>().shape;
        ps = GetComponent<ParticleSystem>();
        myTransform = transform;
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
            else if(shootHit.transform.gameObject.layer == LayerMask.NameToLayer("Projectile"))
            {
                shootHit.transform.gameObject.SetActive(false);
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
        myTransform.position = player.transform.position;

        //Keep particle effect facting crosshair
        Vector3 directionMid = (crosshair.transform.position - myTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionMid);
        myTransform.rotation = lookRotation;


        //This block of code has laser continuously track and move line-renderer//
        Vector3 pos_at_z_0 = crosshair.transform.position;
        Vector2 point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);
        Vector2 currentPos = new Vector2(player.transform.position.x, player.transform.position.y);
        shootRay.origin = player.transform.position;
        shootRay.direction = (point - currentPos);
        //////////////////////////////////////////////////////////////////////

        shootHitPath = Physics.SphereCastAll(shootRay, 17.5f, range, shootableMask);

        foreach (RaycastHit hit in shootHitPath)
        {

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy") && damageEnabled) //damage enemies
            {
                
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                
                if (enemy != null && enemy.gameObject.activeSelf) 
                {
                    Vector3 screenPoint = Camera.main.WorldToViewportPoint(enemy.transform.position);
                    bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

                    if (onScreen)
                    {
                        enemy.DamageEnemy(damage);

                        GameObject hitEffect = ObjectPool.current.getPooledObject(particle);
                        if (hitEffect == null) return;
                        hitEffect.transform.position = hit.point;
                        hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                        hitEffect.SetActive(true);
                    }

                }
            }
            else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Projectile") && damageEnabled) //destroy enemy projectiles
            {
                hit.transform.gameObject.SetActive(false);

                GameObject hitEffect = ObjectPool.current.getPooledObject(particle);
                if (hitEffect == null) return;
                hitEffect.transform.position = hit.point;
                hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                hitEffect.SetActive(true);
            }


        }

      

        gunLine.SetPosition(0, player.transform.position);
        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);

       
        if (growingWidth <= maxWidth - 1f && !grow && !initialFire)
        {
            growingWidth = Mathf.Lerp(growingWidth, 0.5f, Time.deltaTime * 5f);
            gunLine.SetWidth(growingWidth, growingWidth);
            if (!occupied && (growingWidth <= maxWidth - 1f))
            {
                shapeModule.radius = 0.5f;
                ps.startLifetime = 3f;
                StartCoroutine(prepareBlast());
               
            }
        }
        

        if (growingWidth <= maxWidth - 1f && grow)
        {
            shapeModule.radius = 3f;
            ps.startLifetime = 70f;
            CameraScript.camera.ShakeCam(0.3f, 2.3f);
            growingWidth = Mathf.Lerp(growingWidth, maxWidth, Time.deltaTime * 3.5f);
            gunLine.SetWidth(growingWidth, growingWidth);
            if(growingWidth >= maxWidth - 1f && !occupied)
            {
                //shrink = true;
                StartCoroutine(extendBlast());
            }
        }

        if (shrink)
        {
            playerWeapon.firing = false;
            shapeModule.radius = 0.5f;
            ps.startLifetime = 1f;
            damageEnabled = false;
            grow = false;
            //print("BIG");
            growingWidth = Mathf.Lerp(growingWidth, 0, Time.deltaTime * 10f);
            gunLine.SetWidth(growingWidth, growingWidth);
        }

    }

    IEnumerator prepareBlast()
    {
        occupied = true;
        yield return new WaitForSeconds(0.5f);
        damageEnabled = true;
        grow = true;
        //Start decrementing player super gauge
        playerWeapon.charge = false;
        initialFire = true;
        occupied = false;
        
    }

    IEnumerator extendBlast()
    {
        occupied = true;
        yield return new WaitForSeconds(2f);
        shrink = true;
        //Start recharging player super gauge
        playerWeapon.charge = true;
        occupied = false;
    }


}

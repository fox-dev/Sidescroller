using UnityEngine;
using System.Collections;

public class ShootFireBall : MonoBehaviour {

    
    public static int damage = 25;

    [SerializeField]
    private int viewDamage = damage;

    public GameObject particle;

    public float speed;

    public LayerMask collisionMask;

    GameObject player;
    GameObject crosshair;
    GameObject originMid;

    Rigidbody rb;

    Vector2 directionMid, directionUp;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        rb = GetComponent<Rigidbody>();
    }               

    void OnEnable()
    {
         
        if (!GameManager.gm.upgrades.fireBall_x3) //Set fireball up and down inactive if upgrade not purchased.
        {
            if (this.gameObject.tag == "FireBallUp" || this.gameObject.tag == "FireBallDown")
            {
                this.gameObject.SetActive(false);
            }
        }

        viewDamage = damage;

        Vector3 pos_at_z_0 = crosshair.transform.position;

        Vector2 point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        ///////////////////


        if (this.gameObject.tag == "FireBallMid")
        {

            directionMid = (point - currentPos).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionMid);
            transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);

        }

        if (this.gameObject.tag == "FireBallUp")
        {

            point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);
            currentPos = new Vector2(transform.position.x, transform.position.y);

            directionUp = (point - currentPos).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionUp);
            transform.rotation = lookRotation * Quaternion.Euler(3, 0, 0);

            //rb.AddForce(transform.forward * speed, ForceMode.Impulse);
        }

        if (this.gameObject.tag == "FireBallDown")
        {

            point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);
            currentPos = new Vector2(transform.position.x, transform.position.y);

            directionUp = (point - currentPos).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionUp);
            transform.rotation = lookRotation * Quaternion.Euler(-3, 0, 0);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        
        if((collisionMask.value & 1<<other.gameObject.layer) != 0)
        {
            
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(damage);

                }
            }
            if (other.gameObject.layer == LayerMask.NameToLayer("Road"))
            {
                //Instantiate(Resources.Load("HitParticles"), transform.position, Quaternion.identity);
                //rb.velocity = Vector3.zero;
               // transform.position = Vector3.zero;

            }
        }
        
    }

    void OnCollisionEnter(Collision collision)
    {
       
        if((collisionMask.value & 1 << collision.gameObject.layer) != 0)
        {
            GameObject hitEffect = ObjectPool.current.getPooledObject(particle);

            if (hitEffect == null) return;
            hitEffect.transform.position = transform.position;
            hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);

            hitEffect.SetActive(true);

            
            transform.position = Vector3.zero;
            rb.velocity = Vector3.zero;
            transform.gameObject.SetActive(false);

            if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(damage);

                }
            }
        }
       
    }




}

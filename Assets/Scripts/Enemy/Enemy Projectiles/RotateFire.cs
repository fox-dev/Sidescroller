using UnityEngine;
using System.Collections;

public class RotateFire : MonoBehaviour
{
    GameObject weapon;

    public LayerMask collisionMask;

    public int damage;

    public float speed;

    GameObject origin;

    Rigidbody rb;

    Vector2 directionMid, directionUp;

    Quaternion shootDirection;

    // Use this for initialization
    void Start()
    {
        
        /*
        if (this.gameObject.tag == "FireBallUp")
        {

            point = new Vector2(transform.position.x, player.transform.position.y);
            currentPos = new Vector2(transform.position.x, transform.position.y);

            directionUp = (point - currentPos).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionUp);
            transform.rotation = lookRotation * Quaternion.Euler(0, 3, 0);

            rb.AddForce(transform.forward * speed, ForceMode.Impulse);


        }

        if (this.gameObject.tag == "FireBallDown")
        {

            point = new Vector2(transform.position.x, player.transform.position.y);
            currentPos = new Vector2(transform.position.x, transform.position.y);

            directionUp = (point - currentPos).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionUp);
            transform.rotation = lookRotation * Quaternion.Euler(0, -3, 0);

            rb.AddForce(transform.forward * speed, ForceMode.Impulse);


        }
        */



    }


    void OnEnable()
    {
        origin = GameObject.FindGameObjectWithTag("Origin");

        rb = GetComponent<Rigidbody>();

        Vector2 point = new Vector2(transform.position.x, origin.transform.position.y);

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        //Vector2 direction = (point - currentPos).normalized;




        ///////////////////


        if (this.gameObject.tag == "FireBallMid")
        {


            directionMid = (point - currentPos).normalized;
            //shootDirection = transform.GetComponentInParent<Enemy_Weapon>().getShootDirection();

            Quaternion lookRotation = Quaternion.LookRotation(directionMid);
            transform.rotation = lookRotation * shootDirection;



        }
    }
 

    void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            

            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.DamagePlayer(damage);
            }
            else
            {
                Debug.Log("Player object does not exist");
            }
          
          
            rb.velocity = Vector3.zero;
            transform.position = Vector3.zero;
            transform.gameObject.SetActive(false);
            //Destroy(gameObject, 3f);
        }
        /*
        if (other.gameObject.layer == LayerMask.NameToLayer("Road"))
        {

            rb.velocity = Vector3.zero;
            transform.position = Vector3.zero;
            transform.gameObject.SetActive(false);
            //Destroy(gameObject, 3f);
        }
        */
        
    }

    public void assignShootDirection(Quaternion q)
    {

        shootDirection = q;
    }
}

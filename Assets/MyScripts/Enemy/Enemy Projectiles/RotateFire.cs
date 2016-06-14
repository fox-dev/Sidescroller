using UnityEngine;
using System.Collections;

public class RotateFire : MonoBehaviour
{
    GameObject weapon;

    public LayerMask collisionMask;

    public float damage;

    public float speed;

    GameObject player;

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
        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();

        Vector2 point = new Vector2(transform.position.x, player.transform.position.y);

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        //Vector2 direction = (point - currentPos).normalized;




        ///////////////////


        if (this.gameObject.tag == "FireBallMid")
        {


            directionMid = (point - currentPos).normalized;
            //shootDirection = transform.GetComponentInParent<Enemy_Weapon>().getShootDirection();

            Quaternion lookRotation = Quaternion.LookRotation(directionMid);
            transform.rotation = lookRotation * shootDirection;

         

            //transform.rotation = lookRotation * Quaternion.Euler(0, -179, 0);
            if(shootDirection == Quaternion.Euler(0, 90, 0))
            {
                rb.AddForce(transform.forward * (speed + 15), ForceMode.Impulse);
            }
            else
            {
                rb.AddForce(transform.forward * (speed + 0), ForceMode.Impulse);
            }
           
            //transform.parent.parent = null;



        }
    }
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(player.GetComponent<PlayerMovement>().getVelocity() * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce(transform.forward * 300 * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Instantiate(Resources.Load("explosion"), other.transform.position, Quaternion.identity);
            //other.transform.gameObject.SetActive(false);
            rb.velocity = Vector3.zero;
            transform.position = Vector3.zero;
            //Destroy(gameObject, 3f);
        }
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Road"))
        {

            rb.velocity = Vector3.zero;
            transform.position = Vector3.zero;
            //Destroy(gameObject, 3f);
        }
        
    }

    public void assignShootDirection(Quaternion q)
    {

        shootDirection = q;
    }
}

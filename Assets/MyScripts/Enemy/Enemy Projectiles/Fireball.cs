using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour
{
    public LayerMask collisionMask;

    GameObject weapon;

    public float damage;

    public float speed;

    GameObject player;

    Rigidbody rb;

    Vector2 directionMid, directionUp;

    Quaternion shootDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();

        //Vector2 point = new Vector2(transform.position.x, player.transform.position.y);
        Vector2 point = new Vector2(transform.position.x, transform.position.y - 10);

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        //print(transform.position.y + " " + player.transform.position.y);

        print(transform.position);


        ///////////////////


        if (this.gameObject.tag == "FireBallMid")
        {


            directionMid = (point - currentPos).normalized;


            Quaternion lookRotation = Quaternion.LookRotation(directionMid);


            transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);
            //rb.AddForce(transform.forward * speed, ForceMode.Impulse);



        }
    }

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();

        //Vector2 point = new Vector2(transform.position.x, player.transform.position.y);
        Vector2 point = new Vector2(transform.position.x, transform.position.y - 10);

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        //print(transform.position.y + " " + player.transform.position.y);

 
        ///////////////////


        if (this.gameObject.tag == "FireBallMid")
        {


            directionMid = (point - currentPos).normalized;


            Quaternion lookRotation = Quaternion.LookRotation(directionMid);


            transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);
            rb.AddForce(transform.forward * speed, ForceMode.Impulse);



        }
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
   

    // Update is called once per frame
    void Update()
    {
       //print(LayerMask.NameToLayer("Player"));
        //transform.Translate(player.GetComponent<PlayerMovement>().getVelocity() * Time.deltaTime);
        //GetComponent<Rigidbody>().AddForce(transform.forward * 300 * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {

        if ((collisionMask.value & 1 << other.gameObject.layer) != 0)
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
                // Destroy(gameObject, 3f);
            }
        }
       
        
    }

    public void assignShootDirection(Quaternion q)
    {
        
        shootDirection = q;
    }
}

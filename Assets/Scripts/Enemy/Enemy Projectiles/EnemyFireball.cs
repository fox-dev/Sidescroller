using UnityEngine;
using System.Collections;

public class EnemyFireball : MonoBehaviour
{
    public LayerMask collisionMask;

    GameObject weapon;

    public int damage;


    public float speed;

    GameObject player;

    Rigidbody rb;

    Vector2 directionMid, directionUp;

    Quaternion shootDirection;

    [SerializeField]
    public bool targetPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();

        //Vector2 point = new Vector2(transform.position.x, player.transform.position.y);
        Vector2 point = new Vector2(transform.position.x, transform.position.y - 10);
        

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        //print(transform.position.y + " " + player.transform.position.y);

        //print(transform.position);


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
        if (targetPlayer)
        {
            point = new Vector2(player.transform.position.x, player.transform.position.y);
        }
       

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        //print(transform.position.y + " " + player.transform.position.y);

 
        ///////////////////


        if (this.gameObject.tag == "FireBallMid")
        {


            directionMid = (point - currentPos).normalized;


            Quaternion lookRotation = Quaternion.LookRotation(directionMid);


            transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);
            //rb.AddForce(transform.forward * speed, ForceMode.Impulse);



        }

        if (transform.parent.name.Contains("Enemy_Fireballx3"))
        {
            if (this.gameObject.tag == "FireBallUp")
            {

                point = new Vector2(player.transform.position.x, player.transform.position.y);
                currentPos = new Vector2(transform.position.x, transform.position.y);

                directionUp = (point - currentPos).normalized;

                Quaternion lookRotation = Quaternion.LookRotation(directionUp);
                transform.rotation = lookRotation * Quaternion.Euler(3, 0, 0);

              


            }

            if (this.gameObject.tag == "FireBallDown")
            {

                point = new Vector2(player.transform.position.x, player.transform.position.y);
                currentPos = new Vector2(transform.position.x, transform.position.y);

                directionUp = (point - currentPos).normalized;

                Quaternion lookRotation = Quaternion.LookRotation(directionUp);
                transform.rotation = lookRotation * Quaternion.Euler(-3, 0, 0);

                


            }
        }
        
    }
   

    void OnTriggerEnter(Collider other)
    {

        if ((collisionMask.value & 1 << other.gameObject.layer) != 0)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                

                //other.transform.gameObject.SetActive(false);

                Player player = other.GetComponent<Player>();
                if (player != null)
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
            }

            /*
            if (other.gameObject.layer == LayerMask.NameToLayer("Road"))
            {

                rb.velocity = Vector3.zero;
                transform.position = Vector3.zero;
                transform.gameObject.SetActive(false);
                // Destroy(gameObject, 3f);
            }
            */
        }
       
        
    }

    public void assignShootDirection(Quaternion q)
    {
        
        shootDirection = q;
    }
}

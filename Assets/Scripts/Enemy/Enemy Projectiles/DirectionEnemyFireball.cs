using UnityEngine;
using System.Collections;

public class DirectionEnemyFireball : MonoBehaviour
{
    public LayerMask collisionMask;

    GameObject weapon;

    public int damage;

    GameObject player;

    Rigidbody rb;


    [SerializeField]
    public bool targetPlayer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody>();

   
    }

    void OnEnable()
    {


        rb = GetComponent<Rigidbody>();

        if (this.gameObject.tag == "FireBallMid")
        {

            Quaternion lookRotation = Quaternion.LookRotation(-transform.right);


            transform.rotation = lookRotation * Quaternion.Euler(90, 0, 0);

        }

        if (transform.parent.name.Contains("Enemy_Fireballx3"))
        {
            if (this.gameObject.tag == "FireBallUp")
            {

                Quaternion lookRotation = Quaternion.LookRotation(-transform.right);
                transform.rotation = lookRotation * Quaternion.Euler(95, 0, 0);

            }

            if (this.gameObject.tag == "FireBallDown")
            {

                Quaternion lookRotation = Quaternion.LookRotation(-transform.right);
                transform.rotation = lookRotation * Quaternion.Euler(85, 0, 0);

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
}

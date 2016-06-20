﻿using UnityEngine;
using System.Collections;

public class ShootFireBall : MonoBehaviour {

    public int damage;

    public GameObject particle;

    public float speed;

    public LayerMask collisionMask;

    GameObject player;
    GameObject crosshair;
    GameObject originMid;

    Rigidbody rb;

    Vector2 directionMid, directionUp;

    private float velocityCompensation;

    // Use this for initialization
    void Start()
    {
        
    }               
    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        originMid = GameObject.FindGameObjectWithTag("OriginMid");


        //Velocity comepsation for given speed of 15
        if (crosshair.transform.position.x > originMid.transform.position.x)
        {
            velocityCompensation = 10;
        }
        else if (crosshair.transform.position.x <= originMid.transform.position.x)
        {
            velocityCompensation = 5;
        }

        if(player.transform.position.x > originMid.transform.position.x)
        {
            velocityCompensation = 5;
        }
        else if((originMid.transform.position.x - player.transform.position.x) <= 10)
        {
            velocityCompensation = 5;
        }

        if ((crosshair.transform.position.y - player.transform.position.y) < 7)
        {
            velocityCompensation = 0;
        }

        velocityCompensation = 0;
        //  print(velocityCompensation);

        rb = GetComponent<Rigidbody>();

        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //float z_plane_of_2d_game = 0;
        //Vector3 pos_at_z_0 = ray.origin + ray.direction * (z_plane_of_2d_game - ray.origin.z) / ray.direction.z;
        Vector3 pos_at_z_0 = crosshair.transform.position;
        Vector2 point = new Vector2(pos_at_z_0.x + velocityCompensation, pos_at_z_0.y);

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);

        //Vector2 direction = (point - currentPos).normalized;

        ///////////////////


        if (this.gameObject.tag == "FireBallMid")
        {

            directionMid = (point - currentPos).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionMid);
            transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);
            //rb.AddForce(directionMid * speed, ForceMode.Impulse);

        }

        if (this.gameObject.tag == "FireBallUp")
        {

            point = new Vector2(pos_at_z_0.x + velocityCompensation, pos_at_z_0.y);
            currentPos = new Vector2(transform.position.x, transform.position.y);

            directionUp = (point - currentPos).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionUp);
            transform.rotation = lookRotation * Quaternion.Euler(3, 0, 0);

            //rb.AddForce(transform.forward * speed, ForceMode.Impulse);


        }

        if (this.gameObject.tag == "FireBallDown")
        {

            point = new Vector2(pos_at_z_0.x + velocityCompensation, pos_at_z_0.y);
            currentPos = new Vector2(transform.position.x, transform.position.y);

            directionUp = (point - currentPos).normalized;

            Quaternion lookRotation = Quaternion.LookRotation(directionUp);
            transform.rotation = lookRotation * Quaternion.Euler(-3, 0, 0);

            //rb.AddForce(transform.forward * speed, ForceMode.Impulse);


        }
    }

    void OnTriggerEnter(Collider other)
    {
        
        if((collisionMask.value & 1<<other.gameObject.layer) != 0)
        {
            
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                //Instantiate(Resources.Load("HitParticles"), transform.position, Quaternion.identity);
                //print(other.name);
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(damage);
                    //Debug.Log("We hit " + other.name + " and did" + damage + " damage.");

                }
                
                // other.transform.gameObject.SetActive(false);
               //rb.velocity = Vector3.zero;
                //transform.position = Vector3.zero;
                //Destroy(gameObject);
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

            if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(damage);
                    //Debug.Log("We hit " + other.name + " and did" + damage + " damage.");

                }
            }
        }
       
    }




}

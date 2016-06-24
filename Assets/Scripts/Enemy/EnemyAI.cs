﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    Enemy enemy;
    Enemy_Weapon weapon;

    GameObject player, origin;
    public Transform[] path; 

    int currentPoint = 0;
    float proxyDist = 1.0f;

    Vector3 vel;


    float moveSpd = 0.000001f;
    public float flySpd;

    private bool occupied, phase2;

    // Use this for initialization
    void Awake() {
        enemy = this.GetComponent<Enemy>();
        weapon = this.GetComponentInChildren<Enemy_Weapon>();
        player = GameObject.FindGameObjectWithTag("Player");
        origin = GameObject.FindGameObjectWithTag("Origin");

        if (player == null)
        {
            print("Player object not found");
        }

        if (origin == null)
        {
            print("Origin object not found");
        }

    }

   
	
	// Update is called once per frame
	void Update () {


        if (gameObject.tag == "Fly_By")
        {
            Vector3 dir = path[currentPoint].position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, flySpd * Time.deltaTime);
            if (dir.magnitude <= proxyDist)
            {
                currentPoint++;
            }

            if (currentPoint >= path.Length)
            {
                currentPoint = 0;
            }


        }

        else if (enemy.stats.curHealth > 0 && gameObject.name.Contains("Boss_Enemy3"))
        {
            Vector3 dir = path[currentPoint].position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, flySpd * Time.deltaTime);

            if (dir.magnitude <= proxyDist && !occupied && !phase2)
            {
                print("IM HERE");
                StartCoroutine(FocusFire());
            }

        }

        else if (enemy.stats.curHealth > 0 && gameObject.name.Contains("Boss_Enemy2"))
        {
            Vector3 dir = path[currentPoint].position - transform.position;
            transform.position = Vector3.MoveTowards(transform.position, path[currentPoint].position, flySpd * Time.deltaTime);

            if (dir.magnitude <= proxyDist && !occupied && !phase2)
            {
                StartCoroutine(hoverFire());
            }

            if (dir.magnitude <= proxyDist && !occupied && phase2)
            {
                StartCoroutine(spreadFire());
            }

            if (phase2)
            {
                if (dir.magnitude <= proxyDist)
                {
                    if (currentPoint == 0)
                    {
                        currentPoint = 2;
                    }
                    else if (currentPoint == 2)
                    {
                        currentPoint = 0;
                    }
                }
            }


        }
        else if(enemy.stats.curHealth > 0 && gameObject.name.Contains("Boss_Enemy1"))
        {
            vel = origin.GetComponent<Origin>().getVelocity();
            vel = new Vector3(vel.x, 0, 0);
            transform.Translate(vel * Time.deltaTime);

            
            moveSpd = Mathf.Lerp(moveSpd, 100f, Time.deltaTime);
            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 20, player.transform.position.z);
            if (player.GetComponent<PlayerMovement>().maxed_Up)
            {
                //print("HERERERERE1");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(origin.transform.position.x + 68, player.transform.position.y + 25, player.transform.position.z), 15 * Time.deltaTime);

            }

            else if (player.transform.position.x >= transform.position.x && !player.GetComponent<PlayerMovement>().maxed_Up)
            {
                //print("HERERERERE2");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x + 20, player.transform.position.y + 25, player.transform.position.z), 15 * Time.deltaTime);

            }
            else
            {
                //print("HERERERERE3");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(origin.transform.position.x + 10, player.transform.position.y + 25, player.transform.position.z), 15 * Time.deltaTime);
            }
        }
        
    }

    public void assignPath(Transform[] t)
    {
        path = t;
    }
    IEnumerator FocusFire() //Boss3
    {

        occupied = true;

        Animator ani = gameObject.GetComponentInChildren<Animator>();

        if (currentPoint == 0 || currentPoint == 2)
        {
            transform.GetComponentInChildren<Point>().enabled = true;
            ani.SetBool("Attacking", true);
            ani.SetBool("Running", false);
            yield return new WaitForSeconds(0.5f);
            weapon.setFireRate(0.12f);
            weapon.Shoot();
        }
        else //currentPoint == 3
        {
            transform.GetComponentInChildren<Point>().enabled = false;
            ani.SetBool("Focusing", true);
            ani.SetBool("Running", false);
            yield return new WaitForSeconds(0.5f);
            weapon.setFireRate(0.05f);
            weapon.switchToAltFire(); //Switch to true
            weapon.Shoot(); //Start firing
        }
       
       
        
        yield return new WaitForSeconds(5f);


        if (currentPoint == 0 || currentPoint == 2)
        {
            weapon.Shoot();
            ani.SetBool("Attacking", false);
            ani.SetBool("Running", true);
            transform.GetComponentInChildren<Point>().enabled = false;
        }
        else //current == 3
        {
            weapon.switchToAltFire(); //Switch to false
            weapon.Shoot(); //stop firing
            ani.SetBool("Focusing", false);
            ani.SetBool("Running", true);
            transform.GetComponentInChildren<Point>().enabled = false;
        }
       
        Vector3 dir = path[currentPoint].position - transform.position;

        if (dir.magnitude <= proxyDist)
        {
            currentPoint++;
        }

        if (currentPoint >= path.Length)
        {
            currentPoint = 0;
        }

        occupied = false;

      
    }

    IEnumerator hoverFire() //Boss2
    {

        occupied = true;
        if (weapon.rotateInterval == 45)
        {
            weapon.rotateInterval = 10;
   
        }
        else if (weapon.rotateInterval == 10)
        {
            weapon.rotateInterval = 45;
            
        }
        weapon.Shoot();
        Animator ani = gameObject.GetComponentInChildren<Animator>();
        ani.SetBool("fire", true);
        yield return new WaitForSeconds(5f);


        weapon.Shoot();
        ani.SetBool("fire", false);
        
        Vector3 dir = path[currentPoint].position - transform.position;
        
        if (dir.magnitude <= proxyDist)
        {
            currentPoint++;
        }

        if (currentPoint >= path.Length)
        {
            currentPoint = 0;
        }

        occupied = false;

        if (currentPoint == 0)
        {
            phase2 = true;
        }
    }

    IEnumerator spreadFire() //boss2
    {
       
        occupied = true;

        print("in here");
        weapon.rotateInterval = 45;

        weapon.Shoot();
        Animator ani = gameObject.GetComponentInChildren<Animator>();
        ani.SetBool("fire", true);
        

        Vector3 dir = path[currentPoint].position - transform.position;

        yield return new WaitForSeconds(10f);
        weapon.Shoot();
        ani.SetBool("fire", false);
        occupied = false;

        if (currentPoint == 0)
        {
            phase2 = false;
        }

    }

    void OnDisable()
    {
        path = null;
        currentPoint = 0;
    }


}

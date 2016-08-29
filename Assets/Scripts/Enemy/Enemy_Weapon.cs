using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Weapon : MonoBehaviour
{
    Enemy enemy;
    public float timeBetweenBullets = 0.15f;

    public GameObject projectile;

    [Header("Altfre Optional, used for Certain Enemies")]
    public GameObject altFire;

    public float rotateInterval;

    float nextBullet; //when next can be fired

    GameObject temp;

    public float shootInterval = 2f;

    [SerializeField]
    private bool fire = false;

	[SerializeField]
    private bool alternateFire = false;

    Quaternion direction;

    [SerializeField]
    private float shootRotation;

    private GameObject origin; //Object to child projectiles to

    ///////SPECIFICALLY FOR MIRRORBOSS/////
    private Quaternion currentRot, targetRot;
    private GameObject crosshair;
    private Transform myTransform;
    private GameObject player;
    private bool fireLaser = true;
    private bool tracking = true;
    private bool occupied = false; //IENUMERATOR


    // Use this for initialization
    void Awake()
    {

        myTransform = transform;
        origin = GameObject.FindGameObjectWithTag("ESM");
        enemy = transform.parent.GetComponent<Enemy>();
        crosshair = GameObject.FindGameObjectWithTag("EnemyCrosshair");
        player = GameObject.FindGameObjectWithTag("Player");

        reinit(); // reinitialize starting variables

        if (enemy == null) //immediate parent may not be the enemy object and therefore null
        {
            enemy = transform.parent.parent.parent.GetComponent<Enemy>();
        }

        /*if((transform.parent.gameObject.tag != "Kamikaze" || transform.parent.gameObject.tag != "Boss" || transform.parent.gameObject.name.Contains("Boss_Enemy1") || transform.parent.gameObject.name.Contains("Boss_Enemy1")) && !enemy.name.Contains("Boss_Enemy3"))
        {
            InvokeRepeating("Shoot", shootInterval, shootInterval);
        }*/

        /*
        if (transform.parent.gameObject.name.Contains("Boss_Enemy1") || transform.parent.gameObject.name.Contains("Enemy_Fly_Pass") || transform.parent.gameObject.name.Contains("Enemy_Fly_By") || transform.parent.gameObject.name.Contains("Tutorial_Enemy"))
        {
            InvokeRepeating("Shoot", shootInterval, shootInterval);
        }
        */

    }

    void OnDisable()
    {
        reinit();
        CancelInvoke();
    }
    
    void OnEnable()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        reinit();

        if (enemy == null) //immediate parent may not be the enemy object and therefore null
        {
            enemy = transform.parent.parent.parent.GetComponent<Enemy>();
        }
		/* if ((transform.parent.gameObject.tag != "Boss" || transform.parent.gameObject.name.Contains("Boss_Enemy1")) && (!enemy.name.Contains("Boss_Enemy3") && !enemy.name.Contains("MirrorBoss")))
        {
            print("im here");
            InvokeRepeating("Shoot", shootInterval, shootInterval);
        }*/
        
		if (transform.parent.gameObject.name.Contains("Boss_Enemy1") || transform.parent.gameObject.name.Contains("Enemy_Fly_Pass") || transform.parent.gameObject.name.Contains("Enemy_Fly_By"))
		{
			InvokeRepeating("Shoot", shootInterval, shootInterval);
		}

        if (transform.parent.gameObject.name.Contains("Tutorial_Enemy"))
        {
            fire = true;
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        if(crosshair != null && enemy.name.Contains("MirrorBoss") && tracking)
        {
            crosshair.transform.position = Vector3.MoveTowards(crosshair.transform.position, player.transform.position, 30 * Time.deltaTime);

            float AngleRad = Mathf.Atan2(crosshair.transform.position.y - transform.position.y, crosshair.transform.position.x - transform.position.x);

            float AngleDeg = (180 / Mathf.PI) * AngleRad;

            targetRot = Quaternion.Euler(0, 0, AngleDeg + 90);  //+ 90 to properly orient facing position towards player 

            myTransform.rotation = targetRot;
        }



        if (nextBullet < Time.time && fire && enemy.stats.alive)
        {
            if (!alternateFire)
            {
                AudioManager.current.PlaySound("EnemyFire");
            }
            
            
            nextBullet = Time.time + timeBetweenBullets;

            if ((transform.parent.gameObject.tag != "Boss"  || transform.parent.gameObject.name.Contains("Boss_Enemy1") || transform.parent.gameObject.name.Contains("Boss_Enemy3") || transform.parent.gameObject.name.Contains("MirrorBoss")) && !alternateFire)
            {
                
                if (transform.parent.gameObject.name.Contains("Boss_Enemy1"))
                {
                    fireBullet();
                }
                else
                {
                    fireSingleShot(); //Normal enemies and Boss3 and MirrorBoss
                }
                
            }
            else if(!alternateFire)
            {
                fireBullet(); //Boss2

            }
            else if(alternateFire)
            {
                if (enemy.name.Contains("MirrorBoss"))
                {
                    if (fireLaser)
                    {
                        fireLaserBlastAltFire();
                    }
                    
                }
                else
                {
                    fireAltFire();
                }
               
            }
            

          
            //////////////////////

           

        if(shootRotation >= 360)
            {
                shootRotation = 0;
            }
        }


        //temp.GetComponent<LineRenderer>().SetPosition(0, temp.transform.parent.position);

    }


    void fireBullet() //Boss enemy1
    {
        GameObject bullet = ObjectPool.current.getPooledObject(projectile);

        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {
            foreach (Transform child in bullet.transform)
            {
                child.gameObject.SetActive(true);
            }
            direction = Quaternion.Euler(0, shootRotation, 0);
            // bullet.transform.parent = gameObject.transform;

            //bullet.GetComponent<Fireball>().assignShootDirection(direction);
            bullet.GetComponentInChildren<RotateFire>().assignShootDirection(direction);
            shootRotation += rotateInterval;

        }
        bullet.transform.parent = origin.transform;
        bullet.SetActive(true);

        foreach (Transform child in bullet.transform)
        {
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 20, ForceMode.Impulse);
        }
    }
   

    public void fireAltFire() //Used by Boss3
    {
        GameObject bullet = ObjectPool.current.getPooledObject(altFire);

        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {
            foreach (Transform child in bullet.transform)
            {
                child.gameObject.SetActive(true);
            }
            direction = Quaternion.Euler(0, shootRotation, 0);
            // bullet.transform.parent = gameObject.transform;

            //bullet.GetComponent<Fireball>().assignShootDirection(direction);
            bullet.GetComponentInChildren<RotateFire>().assignShootDirection(direction);
            shootRotation += rotateInterval;

        }
        bullet.transform.parent = origin.transform;
        bullet.SetActive(true);

        foreach (Transform child in bullet.transform)
        {
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 20, ForceMode.Impulse);
        }

        /*

        GameObject bullet2 = ObjectPool.current.getPooledObject(altFire);

        if (bullet2 == null) return;

        bullet2.transform.position = transform.position;
        bullet2.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {
            foreach (Transform child in bullet2.transform)
            {
                child.gameObject.SetActive(true);
            }
            direction = Quaternion.Euler(0, shootRotation, 0);
            // bullet.transform.parent = gameObject.transform;

            //bullet.GetComponent<Fireball>().assignShootDirection(direction);
            bullet2.GetComponentInChildren<RotateFire>().assignShootDirection(direction);
            shootRotation += rotateInterval;

        }
        bullet2.transform.parent = origin.transform;
        bullet2.SetActive(true);

        foreach (Transform child in bullet2.transform)
        {
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 25, ForceMode.Impulse);
        }

        GameObject bullet3 = ObjectPool.current.getPooledObject(altFire);

        if (bullet3 == null) return;

        bullet3.transform.position = transform.position;
        bullet3.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {
            foreach (Transform child in bullet3.transform)
            {
                child.gameObject.SetActive(true);
            }
            direction = Quaternion.Euler(0, shootRotation, 0);
            // bullet.transform.parent = gameObject.transform;

            //bullet.GetComponent<Fireball>().assignShootDirection(direction);
            bullet3.GetComponentInChildren<RotateFire>().assignShootDirection(direction);
            shootRotation += rotateInterval;

        }
        bullet3.transform.parent = origin.transform;
        bullet3.SetActive(true);

        foreach (Transform child in bullet3.transform)
        {
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 25, ForceMode.Impulse);
        }

        GameObject bullet4 = ObjectPool.current.getPooledObject(altFire);

        if (bullet4 == null) return;

        bullet4.transform.position = transform.position;
        bullet4.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {
            foreach (Transform child in bullet4.transform)
            {
                child.gameObject.SetActive(true);
            }
            direction = Quaternion.Euler(0, shootRotation, 0);
            // bullet.transform.parent = gameObject.transform;

            //bullet.GetComponent<Fireball>().assignShootDirection(direction);
            bullet4.GetComponentInChildren<RotateFire>().assignShootDirection(direction);
            shootRotation += rotateInterval;

        }
        bullet4.transform.parent = origin.transform;
        bullet4.SetActive(true);

        foreach (Transform child in bullet4.transform)
        {
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 25, ForceMode.Impulse);
        }
        */

    }

    public void fireLaserBlastAltFire() //Used by mirrorBoss
    {
        fireLaser = false;
        if (!occupied)
        {
            StartCoroutine(stopCrosshair());
        }
        
        GameObject bullet = ObjectPool.current.getPooledObject(altFire);

        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;

        bullet.transform.parent = origin.transform;
        bullet.SetActive(true);
    }

    public void resetFireLaser()
    {
        fireLaser = true;
        tracking = true;
    }


    public void fireSingleShot() //Used by Boss1,and normal enemy units
    {
        GameObject bullet = ObjectPool.current.getPooledObject(projectile);

        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
       
        bullet.transform.parent = origin.transform;
        bullet.SetActive(true);


        if (enemy.name.Contains("Boss_Enemy3"))
        {
            foreach (Transform child in bullet.transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 35, ForceMode.Impulse);

            }

        }
        else if (enemy.name.Contains("MirrorBoss"))
        {
            foreach (Transform child in bullet.transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 50, ForceMode.Impulse);

            }
        }
        else
        {
            foreach (Transform child in bullet.transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 15, ForceMode.Impulse);

            }
        }
        

    }

	public void reinit()
	{
		if(transform.parent.tag == "Kamikaze")
		{
			fire = true;
			alternateFire = false;
		}
		else
		{
			fire = alternateFire = false;
		}

		nextBullet = 0f;
		shootRotation = 0;
		direction = Quaternion.Euler(0, shootRotation, 0); //For fireball, rotate along Y-Axis
	}

    public void Shoot()
    {
        fire = !fire;
    }

    public void switchToAltFire()
    {
        alternateFire = !alternateFire;
    }

    public void setFireRate(float f)
    {
        timeBetweenBullets = f;
    }

    IEnumerator stopCrosshair()
    {
        occupied = true;

        yield return new WaitForSeconds(1f);

        tracking = false;
        occupied = false;
    }
		
}

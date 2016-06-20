using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy_Weapon : MonoBehaviour
{
    Enemy enemy;
    public float timeBetweenBullets = 0.15f;
    public GameObject projectile;
    public float rotateInterval;

    float nextBullet; //when next can be fired

    GameObject temp;

    public float shootInterval = 2f;
  
    bool fire;

    Quaternion direction;

    [SerializeField]
    private float shootRotation;

    private GameObject origin;


    // Use this for initialization
    void Awake()
    {
        origin = GameObject.FindGameObjectWithTag("ESM");
        enemy = transform.parent.GetComponent<Enemy>();

        nextBullet = 0f;
        if(transform.parent.gameObject.tag != "Boss" || transform.parent.gameObject.name.Contains("Boss_Enemy1"))
        {
            InvokeRepeating("Shoot", shootInterval, shootInterval);
        }
        
        fire = false;

        shootRotation = 0;
        direction = Quaternion.Euler(0, shootRotation, 0); //For fireball, rotate along Y-Axis
    }

    // Update is called once per frame
    void Update()
    {

        if (nextBullet < Time.time && fire && enemy.stats.alive)
        {

            
            nextBullet = Time.time + timeBetweenBullets;

            if (transform.parent.gameObject.tag != "Boss" || transform.parent.gameObject.name.Contains("Boss_Enemy2"))
            {
                fireSingleShot();
            }
            else
            {
                fireBullets();
            }

            /*
            List<GameObject> spray = new List<GameObject>();
            //temp = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as GameObject;
            for (int x = 0; x < 5; x++) {

                GameObject bullet = ObjectPool.current.getPooledObject(projectile);

                if (bullet == null) return;

                bullet.transform.position = transform.position;
                bullet.transform.rotation = transform.rotation;
                if (gameObject.transform.parent.gameObject.tag != "Fly_By")
                {

                    direction = Quaternion.Euler(0, shootRotation, 0);
                    // bullet.transform.parent = gameObject.transform;

                    //bullet.GetComponent<Fireball>().assignShootDirection(direction);
                    bullet.GetComponentInChildren<RotateFire>().assignShootDirection(direction);
                    shootRotation += rotateInterval;

                }

                spray.Add(bullet);
            }

            
            for(int i = 0; i < spray.Count; i++)
            {
                spray[i].SetActive(true);
            }
            */
            //////////////////////

           

        if(shootRotation >= 360)
            {
                shootRotation = 0;
            }
        }


        //temp.GetComponent<LineRenderer>().SetPosition(0, temp.transform.parent.position);

    }

    void fireBullets()
    {
        GameObject bullet = ObjectPool.current.getPooledObject(projectile);

        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {

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
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 25, ForceMode.Impulse);
        }


        GameObject bullet2 = ObjectPool.current.getPooledObject(projectile);

        if (bullet2 == null) return;

        bullet2.transform.position = transform.position;
        bullet2.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {

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

        GameObject bullet3 = ObjectPool.current.getPooledObject(projectile);

        if (bullet3 == null) return;

        bullet3.transform.position = transform.position;
        bullet3.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {

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

        GameObject bullet4 = ObjectPool.current.getPooledObject(projectile);

        if (bullet4 == null) return;

        bullet4.transform.position = transform.position;
        bullet4.transform.rotation = transform.rotation;
        if (gameObject.transform.parent.gameObject.tag != "Fly_By")
        {

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

    }

    public void fireSingleShot()
    {
        GameObject bullet = ObjectPool.current.getPooledObject(projectile);

        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
       
        bullet.transform.parent = origin.transform;
        bullet.SetActive(true);

        foreach (Transform child in bullet.transform)
        {
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 25, ForceMode.Impulse);
        }

    }

    public  void Shoot()
    {
        fire = !fire;
    }

    public Quaternion getShootDirection()
    {
        return direction;
    }

    public void resetShootRotation()
    {
        shootRotation = 0;
    }



    


}

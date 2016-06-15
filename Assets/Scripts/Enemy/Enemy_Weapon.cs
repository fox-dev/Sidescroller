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


    // Use this for initialization
    void Awake()
    {
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

            fireBullets();

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

        bullet.SetActive(true);

        
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

        bullet2.SetActive(true);

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

        bullet3.SetActive(true);

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

        bullet4.SetActive(true);
        
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

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
    private bool alternateFire = false;

    Quaternion direction;

    [SerializeField]
    private float shootRotation;

    private GameObject origin;


    // Use this for initialization
    void Awake()
    {
        origin = GameObject.FindGameObjectWithTag("ESM");
        enemy = transform.parent.GetComponent<Enemy>();
        if(enemy == null) //immediate parent may not be the enemy object and therefore null
        {
            enemy = transform.parent.parent.parent.GetComponent<Enemy>();
        }

        nextBullet = 0f;
        if((transform.parent.gameObject.tag != "Boss" || transform.parent.gameObject.name.Contains("Boss_Enemy1") || transform.parent.gameObject.name.Contains("Boss_Enemy1")) && !enemy.name.Contains("Boss_Enemy3"))
        {
            
            InvokeRepeating("Shoot", shootInterval, shootInterval);
        }
        else if (transform.root.gameObject.tag != "Boss" || transform.root.gameObject.name.Contains("Boss_Enemy1") || transform.root.gameObject.name.Contains("Boss_Enemy1"))
        {

            InvokeRepeating("Shoot", shootInterval, shootInterval);
        }


        shootRotation = 0;
        direction = Quaternion.Euler(0, shootRotation, 0); //For fireball, rotate along Y-Axis
    }

    // Update is called once per frame
    void Update()
    {

        if (nextBullet < Time.time && fire && enemy.stats.alive)
        {

            
            nextBullet = Time.time + timeBetweenBullets;

            if ((transform.parent.gameObject.tag != "Boss"  || transform.parent.gameObject.name.Contains("Boss_Enemy3")) && !alternateFire)
            {
                fireSingleShot();
            }
            else if(!alternateFire)
            {
                fireBullets();
                //fireSingleShot();
            }
            else if(alternateFire)
            {
                fireAltFire();
            }

          
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
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 25, ForceMode.Impulse);
        }

       

        GameObject bullet2 = ObjectPool.current.getPooledObject(projectile);

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

        GameObject bullet3 = ObjectPool.current.getPooledObject(projectile);

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

        GameObject bullet4 = ObjectPool.current.getPooledObject(projectile);

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

    } //Used by Boss2

    void fireAltFire() //Used by Boss3
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
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 25, ForceMode.Impulse);
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

    public void fireSingleShot() //Used by Boss1,and normal enemy units
    {
        GameObject bullet = ObjectPool.current.getPooledObject(projectile);

        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
       
        bullet.transform.parent = origin.transform;
        bullet.SetActive(true);

        foreach (Transform child in bullet.transform)
        {
            child.gameObject.SetActive(true);
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 35, ForceMode.Impulse);
            
        }

    }

    public void Shoot()
    {
        fire = !fire;
    }

    public void switchToAltFire()
    {
        alternateFire = !alternateFire;
    }

    public Quaternion getShootDirection()
    {
        return direction;
    }

    public void resetShootRotation()
    {
        shootRotation = 0;
    }

    public void setFireRate(float f)
    {
        timeBetweenBullets = f;
    }



    


}

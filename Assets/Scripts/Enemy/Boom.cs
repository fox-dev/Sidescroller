using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

    private GameObject lightSource;


    public GameObject projectile;
    public int rotateInterval;


    
    private int numShots;
    private int maxShots;

    public float timeBetweenBullets = 0.15f;
    float nextBullet; //when next can be fireds

    Quaternion direction;
    float shootRotation;

    void Start()
    {
        lightSource = GameObject.FindGameObjectWithTag("Light");

        nextBullet = 0f;
        shootRotation = 0;
        direction = Quaternion.Euler(shootRotation, 90, 0); //For fireball, rotate along Y-Axis
        rotateInterval = 72;
        maxShots = 360 / rotateInterval;

        

    }
    void Awake()
    {
       
        nextBullet = 0f;
        shootRotation = 0;
        direction = Quaternion.Euler(shootRotation, 90, 0); //For fireball, rotate along Y-Axis

       

    }

    void OnDisable()
    {
        this.gameObject.SetActive(false);
    }


    void Update()
    {
        
        lightSource.GetComponent<Light>().intensity = Mathf.Lerp(lightSource.GetComponent<Light>().intensity, 0, 2 * Time.deltaTime) ;
        if (nextBullet < Time.time && numShots < maxShots) 
        {
            nextBullet = Time.time + timeBetweenBullets;

            GameObject bullet = ObjectPool.current.getPooledObject(projectile);

            if (bullet == null) return;

            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            direction = Quaternion.Euler(shootRotation, 90, 0);
            bullet.GetComponent<ExplosionRay>().assignShootDirection(direction);
            shootRotation += rotateInterval;

            numShots++;

            bullet.SetActive(true);
        }
    }

}
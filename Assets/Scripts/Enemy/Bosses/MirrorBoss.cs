using UnityEngine;
using System.Collections;


//This script functions as the weapon script for the MirrorBoss
public class MirrorBoss : MonoBehaviour {

    public GameObject crosshair;

    private Transform myTransform;

    private Quaternion currentRot, targetRot;

    private GameObject player;
    private GameObject origin; //Object to child projectiles to



    /// Same variables as weapon script ///


    Enemy enemy; //itself
    public float timeBetweenBullets = 0.15f;

    public GameObject projectile;

    public float shootInterval = 2f;

    [SerializeField]
    private bool fire = false;
    private bool alternateFire = false;

    float nextBullet; //when next can be fired



    // Use this for initialization
    void Awake () {

        myTransform = transform;
        enemy = transform.parent.GetComponent<Enemy>();

        player = GameObject.FindGameObjectWithTag("Player");
        origin = GameObject.FindGameObjectWithTag("ESM");

        InvokeRepeating("Shoot", shootInterval, shootInterval);

        currentRot = this.transform.rotation;

    }

    void OnEnable()
    {
        currentRot = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

        crosshair.transform.position = Vector3.MoveTowards(crosshair.transform.position, player.transform.position, 25 * Time.deltaTime);

        if (nextBullet < Time.time && fire && enemy.stats.alive)
        {


            nextBullet = Time.time + timeBetweenBullets;

            fireSingleShot();

        }

        float AngleRad = Mathf.Atan2(crosshair.transform.position.y - transform.position.y, crosshair.transform.position.x - transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        targetRot = Quaternion.Euler(0, 0, AngleDeg + 90);

        myTransform.rotation = targetRot;
    }

    public void Shoot()
    {
        fire = !fire;
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
            child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 50, ForceMode.Impulse);

        }


    }
}

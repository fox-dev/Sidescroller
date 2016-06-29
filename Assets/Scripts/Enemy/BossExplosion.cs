using UnityEngine;
using System.Collections;

public class BossExplosion : MonoBehaviour
{

  

    public float range = 10f;
    public int damage = 50;
    public float maxWidth = 7f;
    public float startWidth = 0.025f;
    public float growingWidth = 0.025f;

    public float startWidth2 = 7f;
    public float growingWidth2 = 7f;

    Ray shootRay;
    RaycastHit shootHit;

    public LayerMask shootableMask;
    LineRenderer gunLine;

    public float x, y;

    Quaternion shootDirection;



    // Use this for initialization
    void Awake()
    {
       

        Vector3 pos_at_z_0 = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        Vector2 point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);
        ////////////////

        gunLine = GetComponent<LineRenderer>();



   



        shootRay.origin = transform.position;
   
        shootRay.direction = shootDirection * Vector3.forward;




        gunLine.SetPosition(0, transform.position); 


   

        if (Physics.SphereCast(shootRay, 1f, out shootHit, range, shootableMask))
        {
            //hit an enemy goes here
            Instantiate(Resources.Load("HitParticles"), shootHit.point, Quaternion.FromToRotation(Vector3.up, shootHit.normal));
            gunLine.SetPosition(1, shootHit.point);
            if (shootHit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                print(shootHit.transform.gameObject.name);
                Enemy enemy = shootHit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(damage);
                    Debug.Log("We hit " + shootHit.collider.name + " and did" + damage + " damage.");

                }

            }
            if (shootHit.transform.gameObject.layer != LayerMask.NameToLayer("Road"))
            {
                //do nothing
            }

        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }



    }

    void OnDisable()
    {
        gunLine.SetWidth(startWidth, 0.025f);

    }
    void OnEnable()
    {
        growingWidth2 = 10;
        growingWidth = 0.025f;
        GetComponent<LineRenderer>().SetPosition(0, Vector3.zero);
        GetComponent<LineRenderer>().SetPosition(1, Vector3.zero);


        //Vector3 pos_at_z_0 = crosshair.transform.position;
        Vector3 pos_at_z_0 = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
        
        ////////////////

        gunLine = GetComponent<LineRenderer>();

   



        shootRay.origin = transform.position;
    
        shootRay.direction = shootDirection * Vector3.forward;

        gunLine.SetPosition(0, transform.position);

        if (Physics.SphereCast(shootRay, 1f, out shootHit, range, shootableMask))
        {
            //hit an enemy goes here
            gunLine.SetPosition(1, shootHit.point);
            Instantiate(Resources.Load("HitParticles"), shootHit.point, Quaternion.FromToRotation(Vector3.up, shootHit.normal));
            if (shootHit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Enemy enemy = shootHit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.DamageEnemy(damage);
                    Debug.Log("We hit " + shootHit.collider.name + " and did " + damage + " damage.");
                }
            }
            if (shootHit.transform.gameObject.layer != LayerMask.NameToLayer("Road"))
            {
                //do nothing.
            }

        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }



    }

    // Update is called once per frame
    void Update()
    {


        
       
        Vector3 pos_at_z_0 = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z);
        Vector2 point = new Vector2(pos_at_z_0.x, pos_at_z_0.y);

     


        shootRay.origin = transform.position;
        //shootRay.direction = Quaternion.Euler(x, 90, 0 ) * Vector3.forward ;
        shootRay.direction = shootDirection * Vector3.forward;


        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //hit an enemy goes here
            gunLine.SetPosition(1, shootHit.point);
            //shootHit.transform.gameObject.SetActive(false);
            print("HITHITHIT");

        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);


        }
        
        gunLine.SetPosition(0, transform.position);
        gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);

        ////

        if (growingWidth <= maxWidth - 1f)
        {
            growingWidth = Mathf.Lerp(growingWidth, maxWidth, Time.deltaTime * 10f);
            gunLine.SetWidth(startWidth, growingWidth);
        }
        else if (growingWidth >= maxWidth - 1f)
        {
            growingWidth2 = Mathf.Lerp(growingWidth2, 0, Time.deltaTime * 5f);
            gunLine.SetWidth(startWidth, growingWidth2);
        }



    }

    public void assignShootDirection(Quaternion q)
    {

        shootDirection = q;
    }


}

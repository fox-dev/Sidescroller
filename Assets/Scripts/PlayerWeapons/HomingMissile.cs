using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomingMissile : MonoBehaviour {


    public static int damage = 25;

    [SerializeField]
    private int viewDamage = damage;

    public GameObject particle;


    public LayerMask collisionMask;

    GameObject player;
    GameObject crosshair;

    GameObject singleTarget;
    GameObject[] targets;

    Rigidbody rb;

    private Transform myTransform;



    void OnEnable()
    {
       

        myTransform = transform;
        viewDamage = damage;
        player = GameObject.FindGameObjectWithTag("Player");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");

        Vector3 directionMid = (crosshair.transform.position - myTransform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(directionMid);
        transform.rotation = lookRotation;

        singleTarget = null;
        targets = null;
        targets = FindGameObjectsWithLayer();

        float dist = Mathf.Infinity;
        Vector3 pos = player.transform.position;

      
        if(targets != null)
        {
            foreach (GameObject index in targets)
            {
                Vector3 diff = index.transform.position - pos;
                float curDist = diff.sqrMagnitude;
                if (curDist < dist)
                {
                    Vector3 screenPoint = Camera.main.WorldToViewportPoint(index.transform.position);
                    bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                    if (onScreen)
                    {
                        singleTarget = index;
                        dist = curDist;
                    }
                    
                }
            }
        }
        

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        if(singleTarget != null && singleTarget.activeSelf)                    
        {
            
            Vector3 directionMid = (singleTarget.transform.position - myTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionMid);
            transform.rotation = lookRotation;
            myTransform.position = Vector3.MoveTowards(myTransform.position, singleTarget.transform.position, 40 * Time.deltaTime);
        }
        else if( singleTarget != null && !singleTarget.activeSelf)
        {
          
            myTransform.position += transform.forward * Time.deltaTime * 40;
           // myTransform.position = Vector3.MoveTowards(myTransform.position, Vector3.right, 40 * Time.deltaTime);

        }
        else
        {
           
            myTransform.position += transform.forward * Time.deltaTime * 40;
        }


        
       // transform.rotation = lookRotation * Quaternion.Euler(0, 0, 0);



    }

    void OnCollisionEnter(Collision collision)
    {

        if ((collisionMask.value & 1 << collision.gameObject.layer) != 0)
        {
            GameObject hitEffect = ObjectPool.current.getPooledObject(particle);

            if (hitEffect == null) return;
            hitEffect.transform.position = transform.position;
            hitEffect.transform.rotation = Quaternion.FromToRotation(Vector3.up, collision.contacts[0].normal);

            hitEffect.SetActive(true);


            transform.position = Vector3.zero;
            rb.velocity = Vector3.zero;
            transform.gameObject.SetActive(false);

            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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

    public GameObject[] FindGameObjectsWithLayer()
    {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> goList = new List<GameObject>();

        for(int x = 0; x < goArray.Length; x++)
        {
            if(goArray[x].layer == LayerMask.NameToLayer("Enemy"))
            {
                goList.Add(goArray[x]);
            }
        }

        if(goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();

    }
}

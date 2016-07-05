
using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

    public float timeBetweenBullets = 0.15f;
    public GameObject projectile;
    private GameObject origin;

    float nextBullet; //when next can be fired

    private bool fire;


    // Use this for initialization
    void Awake () {
        nextBullet = 0f;
        origin = GameObject.FindGameObjectWithTag("Origin");
    }
	
	// Update is called once per frame
	void Update () {

      
        if ( ( Input.GetKey("e") || fire) && nextBullet < Time.time)
        {
         
            nextBullet = Time.time + timeBetweenBullets;

            Rigidbody bullet = ObjectPool.current.getPooledObjectRigidBody(projectile);

            if (bullet == null) return;

            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.transform.parent = origin.transform;
            bullet.gameObject.SetActive(true);

            if (!bullet.name.Contains("Homing"))
            {
                foreach (Transform child in bullet.transform)
                {

                    child.gameObject.SetActive(true);
                    child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 100, ForceMode.Impulse);

                }
            }
            
        }

        if(gameObject.tag != "Buddy")
        {
            if (Input.GetKey("a"))
            {
                projectile = GameManager.gm.weaponList[0];
            }
            if (Input.GetKey("s"))
            {
                projectile = GameManager.gm.weaponList[1];
            }
        }
        

    }

    //For onscreen button usage//
    public void firePressed()
    {
        fire = true;
    }

    public void fireReleased()
    {
        fire = false;
    }


}

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

      
        if ((Input.GetMouseButton(0) || Input.GetKey("e") || fire) && nextBullet < Time.time)
        {
         
            nextBullet = Time.time + timeBetweenBullets;

            GameObject bullet = ObjectPool.current.getPooledObject(projectile);

            if (bullet == null) return;
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.transform.parent = origin.transform;
            bullet.SetActive(true);

            foreach (Transform child in bullet.transform)
            {
                child.gameObject.SetActive(true);
                child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 100, ForceMode.Impulse);
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

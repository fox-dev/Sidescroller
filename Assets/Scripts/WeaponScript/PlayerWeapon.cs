using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

    public float timeBetweenBullets = 0.15f;
    public GameObject projectile;
    private GameObject origin;

    float nextBullet; //when next can be fired

    GameObject temp;


    // Use this for initialization
    void Awake () {
        nextBullet = 0f;
        origin = GameObject.FindGameObjectWithTag("Origin");
    }
	
	// Update is called once per frame
	void Update () {

        if ((Input.GetMouseButton(0) || Input.GetKey("e")) && nextBullet < Time.time)
        {
         
            nextBullet = Time.time + timeBetweenBullets;

            //temp = Instantiate(projectile, transform.position, Quaternion.Euler(new Vector3(0, 90, 0))) as GameObject;

            GameObject bullet = ObjectPool.current.getPooledObject(projectile);

            if (bullet == null) return;
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.transform.parent = origin.transform;
            bullet.SetActive(true);

            foreach (Transform child in bullet.transform)
            {
                child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 100, ForceMode.Impulse);
            }




        }
        

        //temp.GetComponent<LineRenderer>().SetPosition(0, temp.transform.parent.position);

    }


}

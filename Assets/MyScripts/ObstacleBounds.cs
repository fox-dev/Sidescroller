using UnityEngine;
using System.Collections;

public class ObstacleBounds : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            print("Entered");
            transform.position = new Vector3(transform.position.x + 333.58f + 331.11f, transform.position.y - 233.4f - 233.96f, transform.position.z);
        }
    }
}

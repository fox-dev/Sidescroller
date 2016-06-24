using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    bool entered;
    Vector3 targetPos;
    Vector3 startPos;
    GameObject player, origin;

  

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        origin = GameObject.FindGameObjectWithTag("OriginMid");
        entered = false;
        startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        targetPos = new Vector3(transform.position.x, transform.position.y + 8.2f, transform.position.z);
        

    }


    // Update is called once per frame
    void Update () {
       
       
        if (entered)
        {

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, targetPos.y, transform.position.z), 75 * Time.deltaTime);

        }
        
        else
        {
            
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, startPos.y, transform.position.z), 75 * Time.deltaTime);
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            entered = true;
            
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            entered = false;

        }
    }

    public void resetPosition()
    {
        
        transform.position = new Vector3(transform.position.x, targetPos.y - 8.2f, transform.position.z);
        entered = false;
     

    }
}

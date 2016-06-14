using UnityEngine;
using System.Collections;

public class DefaultPlayerMovement : MonoBehaviour {

    float moveSpd = 30f;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void FixedUpdate()
    {

        //if (Input.GetKey("w"))
        //{
        //    transform.Translate((Vector3.forward) * moveSpd * Time.deltaTime);
        //}
        if (Input.GetKey("a"))
        {
            transform.Translate((Vector3.left) * moveSpd * Time.deltaTime);
        }
        //if (Input.GetKey("s"))
        //{
        //   transform.Translate((Vector3.back) * moveSpd * Time.deltaTime);
        //}
        if (Input.GetKey("d"))
        {
            transform.Translate((Vector3.right) * moveSpd * Time.deltaTime);
        }

        if(GetComponent<Rigidbody>().velocity.x > 40f)
        {
            Vector3 newVelocity = GetComponent<Rigidbody>().velocity.normalized;
            newVelocity *= 40f;
            GetComponent<Rigidbody>().velocity = newVelocity;
        }

        print(GetComponent<Rigidbody>().velocity);
        

        

        // transform.Translate((Vector3.left) * moveSpd * Time.deltaTime);

    }
}

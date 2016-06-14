using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject origin, origin2;
    public float flat, desc, climb, current;

	// Use this for initialization
	void Start () {

        flat = 17f;
        desc = -5f;
        climb = 20f;
        current = flat; //starting position
        transform.position = new Vector3(transform.position.x, current, transform.position.z);
            
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = new Vector3(transform.localPosition.x, current, transform.localPosition.z);
        if (origin2.GetComponent<OriginController>().collisions.descendingSlope)
        {
            current = Mathf.Lerp(current, desc, Time.deltaTime * 0.7f);
            if (origin.GetComponent<OriginController>().collisions.descendingSlope)
            {
                //transform.localPosition = new Vector3(transform.localPosition.x, desc, transform.localPosition.z);
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, desc, transform.localPosition.z), Time.deltaTime * 10f);

            }

        }
        else if (origin2.GetComponent<OriginController>().collisions.climbingSlope)
        {
            current = Mathf.Lerp(current, climb, Time.deltaTime * 0.7f);
            if (origin.GetComponent<OriginController>().collisions.climbingSlope)
            {
                //transform.localPosition = new Vector3(transform.localPosition.x, desc, transform.localPosition.z);
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, climb, transform.localPosition.z), Time.deltaTime * 10f);

            }
        }
        else
        {
            current = Mathf.Lerp(current, flat, Time.deltaTime * 0.5f);
            if (!origin.GetComponent<OriginController>().collisions.descendingSlope)
            {
                //transform.localPosition = new Vector3(transform.localPosition.x, flat, transform.localPosition.z);
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, flat, transform.localPosition.z), Time.deltaTime * 10f);
            }
        }
	
	}
}

using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject origin, origin2;
    public float flat, desc, climb, current;

    private Vector3 startPos, endPos;
    private float startSize, endSize;

    private Transform myTransform;

	// Use this for initialization
	void Start () {

        startPos = transform.localPosition;
        startSize = Camera.main.orthographicSize;

        endPos = new Vector3(1f, 10.5f, -61f);
        endSize = 9f;

        myTransform = transform;



        flat = 14f;
        desc = -5f;
        climb = 20f;
        current = flat; //starting position
        transform.localPosition = new Vector3(transform.localPosition.x, current, transform.localPosition.z);
            
	}
	
	// Update is called once per frame

    void FixedUpdate()
    {
        if (GameManager.gm.state == GameManager.gameState.setup)
        {
            myTransform.localPosition = Vector3.Lerp(myTransform.localPosition, endPos, 4f * Time.deltaTime);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, endSize, 4f * Time.deltaTime);
        }
        else
        {
            myTransform.localPosition = Vector3.Lerp(myTransform.localPosition, startPos, 4f * Time.deltaTime);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, startSize, 4f * Time.deltaTime);
        }
    }
    /*
	void Update () {

       
        
        
        transform.localPosition = new Vector3(transform.localPosition.x, current, transform.localPosition.z);
        if (origin2.GetComponent<OriginController>().collisions.descendingSlope)
        {
            current = Mathf.Lerp(current, desc, Time.deltaTime * 0.7f);
            if (origin.GetComponent<OriginController>().collisions.descendingSlope)
            {
                print("down");
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
    */
}

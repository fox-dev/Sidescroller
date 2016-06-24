using UnityEngine;
using System.Collections;

//Used for the FollowRoad on PlayerOrigin, for the moving platforms
public class EndlessRoad : MonoBehaviour
{

    bool entered;
    Vector3 targetPos;
    Vector3 startPos;
    GameObject player, origin;

    public GameObject rotation;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        origin = GameObject.FindGameObjectWithTag("OriginMid");
        entered = false;
        startPos = new Vector3(transform.localPosition.x, transform.localPosition.y - 30f, transform.localPosition.z);
        targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y + 10.2f, transform.localPosition.z);


    }


    // Update is called once per frame
    void Update()
    {
        //startPos = new Vector3(transform.localPosition.x, transform.parent.transform.position.y - 20f, transform.localPosition.z);

        if (entered)
        {

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, targetPos.y, transform.localPosition.z), 300 * Time.deltaTime);

        }

        else
        {

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(transform.localPosition.x, startPos.y, transform.localPosition.z), 75 * Time.deltaTime);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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

        //transform.localPosition = new Vector3(transform.localPosition.x, targetPos.y - 8.2f, transform.localPosition.z);
        entered = false;


    }
}

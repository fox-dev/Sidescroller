using UnityEngine;
using System.Collections;

public class RepeatTheRoad : MonoBehaviour
{

   
    private GameObject origin;
    public GameObject lastBuilding;

    // Use this for initialization
    void Start()
    {
        origin = GameObject.FindGameObjectWithTag("Origin");
   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        //print(onScreen);

        if (!onScreen && (origin.transform.position.x > (lastBuilding.transform.position.x + 10f)))
        {

            if (transform.gameObject.tag == "Platform")
            {
                transform.localPosition = new Vector3(transform.localPosition.x + 330, transform.localPosition.y, transform.localPosition.z);
                foreach (Transform child in transform)
                {
                    child.GetComponent<EndlessRoad>().resetPosition();
                }
            }
          
        }

    }
}

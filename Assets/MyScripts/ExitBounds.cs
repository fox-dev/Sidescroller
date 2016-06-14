using UnityEngine;
using System.Collections;

public class ExitBounds : MonoBehaviour
{
    //public GameObject rm;
    RoadManager roadManager;
    public GameObject ab;

    // Use this for initialization
    void Start()
    {

        roadManager = GameObject.FindGameObjectWithTag("roadManager").GetComponent<RoadManager>();
        //roadManager = rm.GetComponent<RoadManager>();
        if (roadManager == null)
        {
            print("roadManager object does not exist");
        }

    }
   

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //roadManager.setNextRoadSet(this.gameObject.transform.parent.gameObject);
            //gameObject.transform.parent.gameObject.SetActive(false);
            /*
            foreach (Transform child in transform.parent.transform)
            {
                foreach (Transform subchild in child)
                {

                    foreach (Transform subchilds in child)
                    {
                        subchilds.gameObject.SetActive(true);
                    }
                    subchild.gameObject.SetActive(true);
                }
                child.gameObject.SetActive(true);
            }
            */
        }
    }
}

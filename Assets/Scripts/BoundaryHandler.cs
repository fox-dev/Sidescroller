using UnityEngine;
using System.Collections;

public class BoundaryHandler : MonoBehaviour {

    RoadManager roadManager;

    // Use this for initialization
    void Start () {

        roadManager = GameObject.FindGameObjectWithTag("roadManager").GetComponent<RoadManager>();
        
        if (roadManager == null)
        {
            print("roadManager object does not exist");
        }

    }
	
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "AB")
        {
            
            roadManager.setCurrentRoadSet(other.gameObject.transform.parent.gameObject);
            roadManager.AppendRoad();
            print(other.gameObject.transform.parent.gameObject);
        }

        if (other.tag == "EB")
        {
            roadManager.setNextRoadSet(other.gameObject.transform.parent.gameObject);

        }

    }

   
}

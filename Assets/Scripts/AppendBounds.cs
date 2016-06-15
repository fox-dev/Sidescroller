using UnityEngine;
using System.Collections;

public class AppendBounds : MonoBehaviour {

    //public GameObject rm;
    RoadManager roadManager;
    public bool entered;

	// Use this for initialization
	void Start () {
        entered = false;
        roadManager = GameObject.FindGameObjectWithTag("roadManager").GetComponent<RoadManager>();
        //roadManager = rm.GetComponent<RoadManager>();
        if(roadManager == null)
        {
            print("roadManager object does not exist");
        }
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !entered)
        {
            
            //roadManager.AppendRoad();
            //roadManager.setCurrentRoadSet(this.gameObject.transform.parent.gameObject);
            //print(this.gameObject.transform.parent.gameObject);
            
        }
    }

    public void entering()
    {
        entered = true;
    }

    public void reset()
    {
        entered = false;
    }
}

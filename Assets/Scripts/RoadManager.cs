using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour {

    public GameObject nextRoadSet;
    public GameObject currentRoadSet;
    public List<GameObject> inactiveRoadSets;

   

	// Use this for initialization
	void Start () {

        nextRoadSet = inactiveRoadSets[Random.Range(0, inactiveRoadSets.Count)]; //get next initial set piece at start of game
        inactiveRoadSets.Remove(nextRoadSet);



	}
	

    public void AppendRoad()
    {
        //nextRoadSet.SetActive(true);
        //nextRoadSet.transform.position = new Vector3(currentRoadSet.transform.position.x + 270f, currentRoadSet.transform.position.y, currentRoadSet.transform.position.z);
        if(nextRoadSet.tag == "SlopeRoad")
        {
            //Local position of the nextRoadSet's BackEnd has the change in y.position
            nextRoadSet.transform.position = new Vector3(currentRoadSet.transform.Find("FrontEnd").transform.position.x, currentRoadSet.transform.Find("FrontEnd").transform.position.y + nextRoadSet.transform.Find("BackEnd").transform.localPosition.y, currentRoadSet.transform.position.z);
            
        }
       
        else
        {
            nextRoadSet.transform.position = new Vector3(currentRoadSet.transform.Find("FrontEnd").transform.position.x - 1f, currentRoadSet.transform.Find("FrontEnd").transform.position.y, currentRoadSet.transform.position.z);
            
        }


        currentRoadSet = nextRoadSet;
        nextRoadSet.SetActive(true);


    }

    public void setNextRoadSet(GameObject o)
    {
        //nextRoadSet = o;
        o.SetActive(false);
        inactiveRoadSets.Add(o);
        nextRoadSet = inactiveRoadSets[Random.Range(0, inactiveRoadSets.Count)];
        if(currentRoadSet.tag == "SlopeRoad" && nextRoadSet.tag == "SlopeRoad")
        {
            while(nextRoadSet.tag == "SlopeRoad") { nextRoadSet = inactiveRoadSets[Random.Range(0, inactiveRoadSets.Count)]; }
        }
        inactiveRoadSets.Remove(nextRoadSet);
        /*
        foreach (Transform child in nextRoadSet.transform)
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

    public void setCurrentRoadSet(GameObject o)
    {
        currentRoadSet = o;
    }
}

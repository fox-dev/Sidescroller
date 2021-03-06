﻿using UnityEngine;
using System.Collections;

public class BackgroundInView : MonoBehaviour {

    private GameObject origin;
    public GameObject lastBuilding;

    public float locScale = 1;

	// Use this for initialization
	void Start () {
        origin = GameObject.FindGameObjectWithTag("Origin");

	}
	// Update is called once per frame
	void Update () {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);

        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        //print(onScreen);

        if(!onScreen && (origin.transform.position.x > (lastBuilding.transform.position.x + 10f)))
        {
            //print(origin.transform.position.x + "        " + lastBuilding.transform.position.x);
            
            if (transform.gameObject.tag == "Platform")
            {
                transform.position = new Vector3(transform.position.x + 330, transform.position.y, transform.position.z);
                foreach (Transform child in transform)
                {
                    child.GetComponent<Platform>().resetPosition();
                }
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 280 * locScale, transform.position.y, transform.position.z);
            }
        }
	
	}
}

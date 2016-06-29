using UnityEngine;
using System.Collections;

public class FollowObject : MonoBehaviour {

    public Transform playerOrigin;
    public GameObject model;
    private Transform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        
        myTransform.position = new Vector3(myTransform.position.x, playerOrigin.position.y - 1f, myTransform.position.z);
	}


}

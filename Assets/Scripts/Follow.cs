using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        //transform.localRotation = player.transform.localRotation;

        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x - 80, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);

    }
}

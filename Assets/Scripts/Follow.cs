using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

    public GameObject playerOrigin, model, origin, origin2;

    float startY;
    float targetY;

	// Use this for initialization
	void Start () {

        startY = transform.localPosition.y;
        targetY = transform.localPosition.y + 10f;
	
	}
	
	// Update is called once per frame
	void Update () {

       

       // transform.parent.localPosition = new Vector3(transform.parent.localPosition.x, targetY, transform.parent.transform.localPosition.z);
        //transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z), Time.deltaTime);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, localized.y, transform.position.z), 100 * Time.deltaTime);
        //parent.transform.position = new Vector3(parent.transform.position.x, player.transform.position.y, parent.transform.position.z);



    }


}

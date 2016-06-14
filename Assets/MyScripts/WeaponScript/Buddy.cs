using UnityEngine;
using System.Collections;

public class Buddy : MonoBehaviour {

    float y;
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.Euler(0, y += 2, 0);
    }
}

using UnityEngine;
using System.Collections;

public class Buddy : MonoBehaviour {

    float y;
	
    void OnEnable()
    {
        //Set inactive if the upgrade is not purchased
        if(!GameManager.gm.upgrades.buddy)
        {
            this.gameObject.SetActive(false);
        }
    }
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.Euler(0, y += 2, 0);
    }
}

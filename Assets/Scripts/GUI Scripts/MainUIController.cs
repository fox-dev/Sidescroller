using UnityEngine;
using System.Collections;

public class MainUIController : MonoBehaviour {


    public RectTransform playerGUI, BossGUI, upgradeGUI;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(GameManager.gm.state == GameManager.gameState.setup)
        {
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator
            upgradeGUI.gameObject.SetActive(true);

        }
        else
        {
            playerGUI.gameObject.SetActive(true);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator
            upgradeGUI.gameObject.SetActive(false);
        }

	}
}

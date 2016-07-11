using UnityEngine;
using System.Collections;

public class MainUIController : MonoBehaviour {


    public RectTransform playerGUI, BossGUI, upgradeGUI, resultsGUI, GetReadyGUI;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.gm.state == GameManager.gameState.setup)
        {
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(true);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script

        }
        else if(GameManager.gm.state == GameManager.gameState.results)
        {
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(true);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
        }
        else
        {
            playerGUI.gameObject.SetActive(true);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
        }

    }
}

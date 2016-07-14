using UnityEngine;
using System.Collections;

public class MainUIController : MonoBehaviour {


    public RectTransform titleScreenGUI, playerGUI, BossGUI, upgradeGUI, resultsGUI, GetReadyGUI;


    // Update is called once per frame
    void Update() {
        if (GameManager.gm.state == GameManager.gameState.menu)
        {
            titleScreenGUI.gameObject.SetActive(true);
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
        }
        else if (GameManager.gm.state == GameManager.gameState.setup)
        {
            titleScreenGUI.gameObject.SetActive(false);
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(true);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script

        }
        else if(GameManager.gm.state == GameManager.gameState.results)
        {
            titleScreenGUI.gameObject.SetActive(false);
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(true);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
        }
        else
        {
            titleScreenGUI.gameObject.SetActive(false);
            playerGUI.gameObject.SetActive(true);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
        }

    }
}

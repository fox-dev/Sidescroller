using UnityEngine;
using System.Collections;

public class MainUIController : MonoBehaviour {


    public RectTransform titleScreenGUI, tutorialScreenGUI, playerGUI, BossGUI, upgradeGUI, resultsGUI, GetReadyGUI, gameOverGUI;


    // Update is called once per frame
    void Update() {
        if (GameManager.gm.state == GameManager.gameState.menu)
        {
            titleScreenGUI.gameObject.SetActive(true);
            tutorialScreenGUI.gameObject.SetActive(false);
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
            gameOverGUI.gameObject.SetActive(false);
        }
        else if(GameManager.gm.state == GameManager.gameState.tutorial_1 || GameManager.gm.state == GameManager.gameState.tutorial_2 || GameManager.gm.state == GameManager.gameState.tutorial_3)
        {
            titleScreenGUI.gameObject.SetActive(false);
            tutorialScreenGUI.gameObject.SetActive(true);
            playerGUI.gameObject.SetActive(true);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
            gameOverGUI.gameObject.SetActive(false);
        }
        else if (GameManager.gm.state == GameManager.gameState.setup)
        {
            titleScreenGUI.gameObject.SetActive(false);
            tutorialScreenGUI.gameObject.SetActive(false);
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(true);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
            gameOverGUI.gameObject.SetActive(false);

        }
        else if(GameManager.gm.state == GameManager.gameState.results)
        {
            titleScreenGUI.gameObject.SetActive(false);
            tutorialScreenGUI.gameObject.SetActive(false);
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(true);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
            gameOverGUI.gameObject.SetActive(false);
        }
        else if(GameManager.gm.state == GameManager.gameState.gameOver)
        {
            titleScreenGUI.gameObject.SetActive(false);
            tutorialScreenGUI.gameObject.SetActive(false);
            playerGUI.gameObject.SetActive(false);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
            gameOverGUI.gameObject.SetActive(true);
        }
        else //Test state
        {
            titleScreenGUI.gameObject.SetActive(false);
            tutorialScreenGUI.gameObject.SetActive(false);
            playerGUI.gameObject.SetActive(true);
            //BossGUI.gameObject.SetActive(false); boss gui handled by animator EnemySpawnManager script
            upgradeGUI.gameObject.SetActive(false);
            resultsGUI.gameObject.SetActive(false);
            //GetReadyGUI GetReadyGUI script handles itself through animator in GameManager script
            gameOverGUI.gameObject.SetActive(false);
        }

    }
}

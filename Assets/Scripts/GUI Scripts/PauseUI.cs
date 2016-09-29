using UnityEngine;
using System.Collections;

public class PauseUI : MonoBehaviour {

   // public RectTransform myTransform;
    

    //return to setup;
    public void returnToSetup()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;

        GameManager.clearScreenOfEnemies();
        GameManager.respawnPlayer();
        GameManager.resetBossFlags();

        StopAllCoroutines();

        if (GameManager.gm.state == GameManager.gameState.tutorial_1 || GameManager.gm.state == GameManager.gameState.tutorial_2 || GameManager.gm.state == GameManager.gameState.tutorial_3)
        {
            GameManager.gm.state = GameManager.gameState.setup;
        }
        else
        {
            GameManager.gm.state = GameManager.gameState.results;
        }
        

        GameManager.turnOffAds();

    }

    //return to setup;
    public void returnToTitle()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;

        GameManager.clearScreenOfEnemies();
        GameManager.respawnPlayer();
        GameManager.resetBossFlags();
        GameManager.gm.state = GameManager.gameState.menu;

        GameManager.turnOffAds();

        StopAllCoroutines();

    }

    public void Pause()
    {
        if (Time.timeScale == 0)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1;
            
        }
        else
        {
            this.gameObject.SetActive(true);
            Time.timeScale = 0;
            
        }

    }


}

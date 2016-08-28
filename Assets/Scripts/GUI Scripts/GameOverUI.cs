using UnityEngine;
using System.Collections;

public class GameOverUI : MonoBehaviour {


    public void retry()
    {
        GameManager.respawnPlayer();

        if (GameManager.gm.prevState == GameManager.gameState.normalPlay) //If player died during normal play;
        {
            GameManager.gm.state = GameManager.gameState.ready;
        }
        else if(GameManager.gm.prevState == GameManager.gameState.bossFight)
        {
            GameManager.gm.state = GameManager.gameState.bossFight;
        }
        
		GameManager.turnOffAds ();

    }

    public void setup()
    {
        GameManager.clearScreenOfEnemies();
        GameManager.respawnPlayer();
        GameManager.gm.state = GameManager.gameState.results;

		GameManager.turnOffAds ();
    }
}

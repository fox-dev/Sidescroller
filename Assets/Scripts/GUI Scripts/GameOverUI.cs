using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverUI : MonoBehaviour {

    [SerializeField]
    private Button retryButton;

    public int retryCost = 2500; //retry cost

    void OnEnable()
    {
        if(GameManager.currency >= retryCost)
        {
            retryButton.interactable = true;
        }
        else
        {
            retryButton.interactable = false;
        }
    }


    public void retry()
    {
      

        if(GameManager.currency >= retryCost)
        {
            GameManager.SubtractCurrency(retryCost);
            GameManager.respawnPlayer();

            if (GameManager.gm.prevState == GameManager.gameState.normalPlay) //If player died during normal play;
            {
                GameManager.gm.state = GameManager.gameState.ready;
            }
            else if (GameManager.gm.prevState == GameManager.gameState.bossFight)
            {
                GameManager.gm.state = GameManager.gameState.bossFight;
            }
        }
        else
        {
            retryButton.interactable = false;
        }

        
        
		GameManager.getNewAds ();
		GameManager.turnOffAds ();

    }

    public void setup()
    {
       
        GameManager.clearScreenOfEnemies();
        GameManager.respawnPlayer();
        GameManager.resetBossFlags();
        GameManager.gm.state = GameManager.gameState.results;

		GameManager.turnOffAds ();

    }
}

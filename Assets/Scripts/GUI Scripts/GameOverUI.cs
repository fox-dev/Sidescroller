using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverUI : MonoBehaviour {

    [SerializeField]
    private Button retryButton;
    [SerializeField]
    private Button setupButton;

    //Values set in inspector
    public int retryCost; //retry cost
    public int setupCost; //cost to return to setup

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

        if (GameManager.currency >= setupCost)
        {
            setupButton.interactable = true;
        }
        else
        {
            setupButton.interactable = false;
        }

        retryButton.GetComponentInChildren<Text>().text = "R E T R Y \n" + retryCost.ToString() + " NRG";
        setupButton.GetComponentInChildren<Text>().text = "S E T U P \n" + setupCost.ToString() + " NRG";
    }


    public void retry()
    {
      

        if(GameManager.currency >= retryCost)
        {
            GameManager.SubtractCurrency(retryCost);
            GameManager.respawnPlayer();

            if (GameManager.gm.prevState == GameManager.gameState.normalPlay) //If player died during normal play;
            {
                GameManager.resetScore();
                GameManager.gm.state = GameManager.gameState.ready;
            }
            else if (GameManager.gm.prevState == GameManager.gameState.bossFight)
            {
                GameManager.resetScore();
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
        if (GameManager.currency >= setupCost)
        {
            GameManager.SubtractCurrency(setupCost);
            GameManager.clearScreenOfEnemies();
            GameManager.respawnPlayer();
            GameManager.resetBossFlags();
            GameManager.gm.state = GameManager.gameState.results;

            GameManager.getNewAds();
            GameManager.turnOffAds();

            
        }
        else
        {
            retryButton.interactable = false;
        }
    }

    public void quit()
    {
        //Reset all player preferences and load
        
        GameManager.gm.resetPrefs();

        //Clear screen of enemies and reset score
        GameManager.clearScreenOfEnemies();
        GameManager.respawnPlayer();
        GameManager.resetBossFlags();
        GameManager.resetScore();

        //Get new ads
        GameManager.getNewAds();
      

        //Return to menu
        GameManager.gm.state = GameManager.gameState.menu;

        GameManager.turnOffAds();

        StopAllCoroutines();


    }

    /*
    public void setup()
    {
       
        GameManager.clearScreenOfEnemies();
        GameManager.respawnPlayer();
        GameManager.resetBossFlags();
        GameManager.gm.state = GameManager.gameState.results;

		GameManager.getNewAds ();
		GameManager.turnOffAds ();

    }
    */
}

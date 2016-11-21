using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseUI : MonoBehaviour {

    [SerializeField]
    private Button setupButton;
    [SerializeField]
    private Button menuButton; //provides same function as setup;
    [SerializeField]
    private Button quitButton;

    //Values set in inspector
    public int setupCost; //cost to return to setup

    public void OnEnable()
    {
        //Change text of buttons depending on current state;
        if (GameManager.gm.state == GameManager.gameState.tutorial_1 || GameManager.gm.state == GameManager.gameState.tutorial_2 || GameManager.gm.state == GameManager.gameState.tutorial_3)
        {
            //Only the menu button is active when pausing on the tutorial screen, it also has no cost;
            setupButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
          
            menuButton.GetComponentInChildren<Text>().text = "M E N U";

        }
        else
        {
            //All pause buttons are active during normal gameplay, and buttons have setupCost;
            setupButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);

            setupButton.GetComponentInChildren<Text>().text = "S E T U P \n" + setupCost.ToString() + " NRG";
            menuButton.GetComponentInChildren<Text>().text = "M E N U \n" + setupCost.ToString() + " NRG";
        }


        if (GameManager.currency >= setupCost)
        {
            setupButton.interactable = true;
            menuButton.interactable = true;
        }
        else
        {
            setupButton.interactable = false;
            menuButton.interactable = false;
        }
    }


    //return to setup;
    public void returnToSetup()
    {
        //Close pause, return timescale to default
        this.gameObject.SetActive(false);
        Time.timeScale = 1;

        StopAllCoroutines();

        //If in tutorial states
        if (GameManager.gm.state == GameManager.gameState.tutorial_1 || GameManager.gm.state == GameManager.gameState.tutorial_2 || GameManager.gm.state == GameManager.gameState.tutorial_3)
        {
            GameManager.clearScreenOfEnemies();
            GameManager.respawnPlayer();
            GameManager.resetBossFlags();
            GameManager.gm.state = GameManager.gameState.setup;

        }//all other states
        else
        {
            if (GameManager.currency >= setupCost)
            {
                GameManager.SubtractCurrency(setupCost);
                GameManager.clearScreenOfEnemies();
                GameManager.respawnPlayer();
                GameManager.resetBossFlags();
                GameManager.gm.state = GameManager.gameState.results;

            }
        }
        StopAllCoroutines();
        GameManager.getNewAds();
        GameManager.turnOffAds();

    }

    //return to setup;
    public void returnToTitle()
    {
        this.gameObject.SetActive(false);
        Time.timeScale = 1;


        //If in tutorial states
        if (GameManager.gm.state == GameManager.gameState.tutorial_1 || GameManager.gm.state == GameManager.gameState.tutorial_2 || GameManager.gm.state == GameManager.gameState.tutorial_3)
        {
            GameManager.clearScreenOfEnemies();
            GameManager.respawnPlayer();
            GameManager.resetBossFlags();
            GameManager.resetScore();
            GameManager.gm.state = GameManager.gameState.menu;
        }
        else
        {
            if (GameManager.currency >= setupCost)
            {
                GameManager.SubtractCurrency(setupCost);
                GameManager.clearScreenOfEnemies();
                GameManager.respawnPlayer();
                GameManager.resetBossFlags();
                GameManager.resetScore();
                GameManager.gm.state = GameManager.gameState.menu;

            }
        }

        StopAllCoroutines();
        GameManager.getNewAds();
        GameManager.turnOffAds();
    }

    //quit session, clear everything
    public void quit()
    {

        this.gameObject.SetActive(false);
        Time.timeScale = 1;

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

    public void Pause()
    {
        if(GameManager.gm.state == GameManager.gameState.normalPlay || GameManager.gm.state == GameManager.gameState.bossFight || GameManager.gm.state == GameManager.gameState.tutorial_1 || GameManager.gm.state == GameManager.gameState.tutorial_2 || GameManager.gm.state == GameManager.gameState.tutorial_3)
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


}

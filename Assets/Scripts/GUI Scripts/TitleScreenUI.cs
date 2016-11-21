using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleScreenUI : MonoBehaviour {

    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button continueButton;





    //For "continue" button use, prefs aren't cleared
    public void startGame()
    {
        GameManager.gm.state = GameManager.gameState.setup;

        //new session available, continue disabled, else enabled
        if (PlayerPrefs.GetInt("NewSession") == 0)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    public void OnEnable()
    {
        //new session available, continue disabled, else enabled
        if(PlayerPrefs.GetInt("NewSession") == 0)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    public void newGame()
    {
        GameManager.gm.initPrefs();
        GameManager.gm.state = GameManager.gameState.setup;
    }

    public void startTutorial()
    {
        GameManager.gm.state = GameManager.gameState.tutorial_1;
    }

}

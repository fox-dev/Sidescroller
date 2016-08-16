using UnityEngine;
using System.Collections;

public class TitleScreenUI : MonoBehaviour {

    
    public void startGame()
    {
        GameManager.gm.state = GameManager.gameState.setup;
    }

    public void startTutorial()
    {
        GameManager.gm.state = GameManager.gameState.tutorial_1;
    }

}

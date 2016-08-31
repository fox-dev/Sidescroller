using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SideGradesPanel : MonoBehaviour {
    [SerializeField]
    private GameObject player;

    private GameObject buddy;

    [SerializeField]
    private Button buddy_purcahseButton;
    private Text buddy_purchaseText;

    [SerializeField]
    private Button fireBallUpgrade_purcahseButton;
    private Text fireBallUpgrade_purchaseText;



    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        buddy_purchaseText = buddy_purcahseButton.GetComponentInChildren<Text>();
        fireBallUpgrade_purchaseText = fireBallUpgrade_purcahseButton.GetComponentInChildren<Text>();

        foreach (Transform child in player.transform)
        {
            if(child.name == "Buddy")
            {
                buddy = child.gameObject;
            }
        }

    }

    public void purcahseBuddy()
    {
        int cost = 10000;

        if(GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            GameManager.gm.upgrades.enableBuddy();
            buddy.SetActive(true);

            buddy_purcahseButton.interactable = false;
            buddy_purchaseText.text = "PURCHASED";
        }
        
    }

    public void purcahseFireBallUpgrade()
    {
        int cost = 5000;

        if (GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            GameManager.gm.upgrades.enableFireBall_x3();

            fireBallUpgrade_purcahseButton.interactable = false;
            fireBallUpgrade_purchaseText.text = "PURCHASED";
        }

    }


}

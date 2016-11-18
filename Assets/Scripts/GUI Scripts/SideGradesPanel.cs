using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SideGradesPanel : MonoBehaviour {
    [SerializeField]
    private GameObject player;

    
    //Buddy
    [SerializeField]
    private Button buddy_purchaseButton;
    private Text buddy_purchaseText;

    //Fireball
    [SerializeField]
    private Button fireBallUpgrade_purchaseButton;
    private Text fireBallUpgrade_purchaseText;

    //Beam
    [SerializeField]
    private Button beamPierceUpgrade_purchaseButton;
    private Text beamPierceUpgrade_purchaseText;

    //Missiles
    [SerializeField]
    private Button rocketRateUpgrade_purchaseButton;
    private Text rocketRateUpgrade_purchaseText;



    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        buddy_purchaseText = buddy_purchaseButton.GetComponentInChildren<Text>();
        fireBallUpgrade_purchaseText = fireBallUpgrade_purchaseButton.GetComponentInChildren<Text>();
        beamPierceUpgrade_purchaseText = beamPierceUpgrade_purchaseButton.GetComponentInChildren<Text>();
        rocketRateUpgrade_purchaseText = rocketRateUpgrade_purchaseButton.GetComponentInChildren<Text>();



    }

    void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        buddy_purchaseText = buddy_purchaseButton.GetComponentInChildren<Text>();
        fireBallUpgrade_purchaseText = fireBallUpgrade_purchaseButton.GetComponentInChildren<Text>();
        beamPierceUpgrade_purchaseText = beamPierceUpgrade_purchaseButton.GetComponentInChildren<Text>();
        rocketRateUpgrade_purchaseText = rocketRateUpgrade_purchaseButton.GetComponentInChildren<Text>();

        if (PlayerPrefs.HasKey("WeaponSidegrades"))
        {
            //Weapon sidegrades have been purchased, load which sidegrades have been purchased
            if (PlayerPrefs.GetInt("WeaponSidegrades") == 1)
            {
                if (PlayerPrefs.GetInt("BuddyPurchased") == 1)
                {
                    GameManager.gm.upgrades.enableBuddy();
                    

                    buddy_purchaseButton.interactable = false;
                    buddy_purchaseText.text = "PURCHASED";
                }

                if (PlayerPrefs.GetInt("FireBallUpgradePurchased") == 1)
                {
                    GameManager.gm.upgrades.enableFireBall_x3();

                    fireBallUpgrade_purchaseButton.interactable = false;
                    fireBallUpgrade_purchaseText.text = "PURCHASED";
                }

                if (PlayerPrefs.GetInt("BeamUpgradePurchased") == 1)
                {
                    GameManager.gm.upgrades.enablePierceBeam();

                    beamPierceUpgrade_purchaseButton.interactable = false;
                    beamPierceUpgrade_purchaseText.text = "PURCHASED";
                }
                
                if (PlayerPrefs.GetInt("RocketUpgradePurchased") == 1)
                {
                    GameManager.gm.upgrades.enableRocketRate();

                    rocketRateUpgrade_purchaseButton.interactable = false;
                    rocketRateUpgrade_purchaseText.text = "PURCHASED";
                }
                

            }
        }
        else
        {
            //Weapon sidegrades have not yet been purchased, now initialize the player prefs and set the sidegrades to 0;
            PlayerPrefs.SetInt("WeaponSidegrades", 1);
            PlayerPrefs.SetInt("BuddyPurchased", 0);
            PlayerPrefs.SetInt("FireBallUpgradePurchased", 0);
            PlayerPrefs.SetInt("BeamUpgradePurchased", 0);
            PlayerPrefs.SetInt("RocketUpgradePurchased", 0);

            buddy_purchaseButton.interactable = true;
            buddy_purchaseText.text = "UPGRADE\n" + "10000 NRG";
            GameManager.gm.upgrades.disableBuddy();

            fireBallUpgrade_purchaseButton.interactable = true;
            fireBallUpgrade_purchaseText.text = "UPGRADE\n" + "5000 NRG";
            GameManager.gm.upgrades.disableFireBall_x3();

            beamPierceUpgrade_purchaseButton.interactable = true;
            beamPierceUpgrade_purchaseText.text = "UPGRADE\n" + "5000 NRG";
            GameManager.gm.upgrades.disablePierceBeam();
            
            rocketRateUpgrade_purchaseButton.interactable = true;
            rocketRateUpgrade_purchaseText.text = "UPGRADE\n" + "5000 NRG";
            GameManager.gm.upgrades.disableRocketRate();
            
        }
    }

    public void purchaseBuddy()
    {
        int cost = 10000;

        if(GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            GameManager.gm.upgrades.enableBuddy();
          

            buddy_purchaseButton.interactable = false;
            buddy_purchaseText.text = "PURCHASED";

            PlayerPrefs.SetInt("BuddyPurchased", 1);
            GameManager.savePref();
        }
        else
        {
            AudioManager.current.playNEGATIVE();
        }

    }

    public void purchaseFireBallUpgrade()
    {
        int cost = 5000;

        if (GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            GameManager.gm.upgrades.enableFireBall_x3();

            fireBallUpgrade_purchaseButton.interactable = false;
            fireBallUpgrade_purchaseText.text = "PURCHASED";

            PlayerPrefs.SetInt("FireBallUpgradePurchased", 1);
            GameManager.savePref();
        }
        else
        {
            AudioManager.current.playNEGATIVE();
        }

    }

    public void purchasePierceBeam()
    {
        int cost = 5000;

        if (GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            GameManager.gm.upgrades.enablePierceBeam();

            beamPierceUpgrade_purchaseButton.interactable = false;
            beamPierceUpgrade_purchaseText.text = "PURCHASED";

            PlayerPrefs.SetInt("BeamUpgradePurchased", 1);
            GameManager.savePref();
        }
        else
        {
            AudioManager.current.playNEGATIVE();
        }

    }

    public void purchaseRocketRate()
    {
        int cost = 5000;

        if (GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            GameManager.gm.upgrades.enableRocketRate();

            rocketRateUpgrade_purchaseButton.interactable = false;
            rocketRateUpgrade_purchaseText.text = "PURCHASED";

            PlayerPrefs.SetInt("RocketUpgradePurchased", 1);
            GameManager.savePref();
        }
        else
        {
            AudioManager.current.playNEGATIVE();
        }

    }




}

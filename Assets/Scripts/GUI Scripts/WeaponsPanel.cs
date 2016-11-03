using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponsPanel : MonoBehaviour {

    //List of weapons
    [SerializeField]
    private Text fireBallText;
    [SerializeField]
    private Text beamText;
    [SerializeField]
    private Text homingMissileText;

    //List of button Texts
    [SerializeField]
    private Text fbCost;
    [SerializeField]
    private Text beamCost;
    [SerializeField]
    private Text hmCost;

    public int upgradeFireBallCost, upgradeBeamCost, upgradeHomingMissileCost;
	private int[] upgradeCount;

	public int upgradeFireBallDmg, upgradeBeamDmg, upgradeHomingMissileDmg;

    // Use this for initialization
    void Start () {
       //PlayerPrefs.DeleteAll();
        

        if(PlayerPrefs.HasKey("DamageUpgrades"))
        { 

            //A weapon has been upgrade, load upgrades from PlayerPrefs
            //Weapon keys//
            //fbCount//fbDmg//
            //beamCount//beamDmg//
            //hmCount//hmDmg//
            upgradeCount = new int[3];
            upgradeCount[0] = PlayerPrefs.GetInt("fbCount");
            upgradeCount[1] = PlayerPrefs.GetInt("beamCount");
            upgradeCount[2] = PlayerPrefs.GetInt("hmCount");

            ShootFireBall.damage = PlayerPrefs.GetInt("fbDmg");
            Beam.damage = PlayerPrefs.GetInt("beamDmg");
            HomingMissile.damage = PlayerPrefs.GetInt("hmDmg");

            fireBallText.text = "DAMAGE: " + ShootFireBall.damage.ToString();
            beamText.text = "DAMAGE: " + Beam.damage.ToString();
            homingMissileText.text = "DAMAGE: " + HomingMissile.damage.ToString();

            fbCost.text = "UPGRADE\n" + PlayerPrefs.GetInt("fbCost").ToString() + " NRG";
            beamCost.text = "UPGRADE\n" + PlayerPrefs.GetInt("beamCost").ToString() + " NRG";
            hmCost.text = "UPGRADE\n" + PlayerPrefs.GetInt("hmCost").ToString() + " NRG";
        }
        else
        {
            //Weapons have not been upgraded yet, instantiate upgrade counts array and PlayerPrefs Keys
            PlayerPrefs.SetInt("DamageUpgrades", 1); //True
            upgradeCount = new int[3];
            for (int i = 0; i < upgradeCount.Length; i++)
            {
                upgradeCount[i] = 0;
            }
            //Weapon keys//
            //fbCount//fbDmg//
            //beamCount//beamDmg//
            //hmCount//hmDmg//
            PlayerPrefs.SetInt("fbCount", upgradeCount[0]);
            PlayerPrefs.SetInt("beamCount", upgradeCount[1]);
            PlayerPrefs.SetInt("hmCount", upgradeCount[2]);

            PlayerPrefs.SetInt("fbDmg", ShootFireBall.damage);
            PlayerPrefs.SetInt("beamDmg", Beam.damage);
            PlayerPrefs.SetInt("hmDmg", HomingMissile.damage);

            PlayerPrefs.SetInt("fbCost", upgradeFireBallCost);
            PlayerPrefs.SetInt("beamCost", upgradeBeamCost);
            PlayerPrefs.SetInt("hmCost", upgradeHomingMissileCost);

            fireBallText.text = "DAMAGE: " + ShootFireBall.damage.ToString();
            beamText.text = "DAMAGE: " + Beam.damage.ToString();
            homingMissileText.text = "DAMAGE: " + HomingMissile.damage.ToString();

            fbCost.text = "UPGRADE\n" + upgradeFireBallCost + " NRG";
            beamCost.text = "UPGRADE\n" + upgradeBeamCost + " NRG";
            hmCost.text = "UPGRADE\n" + upgradeHomingMissileCost + " NRG";
        }
           

     

       
    }

    public void upgradeFireBallDamage()
    {
		int upgradeFireBallCostScaled = upgradeFireBallCost + upgradeCount [0] * 100;

        if (GameManager.currency >= upgradeFireBallCostScaled)
        {
            GameManager.SubtractCurrency(upgradeFireBallCostScaled);
            ShootFireBall.damage += upgradeFireBallDmg;
            fireBallText.text = "DAMAGE: " + ShootFireBall.damage.ToString();
            fbCost.text = "UPGRADE\n" + (upgradeFireBallCost + upgradeFireBallCostScaled).ToString() + " NRG";

            upgradeCount[0]++;
            PlayerPrefs.SetInt("fbCount", upgradeCount[0]);
            PlayerPrefs.SetInt("fbDmg", ShootFireBall.damage);
            PlayerPrefs.SetInt("fbCost", upgradeFireBallCost + upgradeFireBallCostScaled);

            GameManager.savePref();

            AudioManager.current.playUPGRADE();
        }
        else
        {
            AudioManager.current.playNEGATIVE();
        }
            

    }

    public void upgradeBeamDamage()
	{
		int upgradeBeamCostScaled = upgradeBeamCost + upgradeCount [1] * 100;

		if (GameManager.currency >= upgradeBeamCostScaled)
        {
            GameManager.SubtractCurrency(upgradeBeamCostScaled);
			Beam.damage += upgradeBeamDmg;
            beamText.text = "DAMAGE: " + Beam.damage.ToString();
            beamCost.text = "UPGRADE\n" + (upgradeBeamCost + upgradeBeamCostScaled).ToString() + " NRG";

            upgradeCount[1]++;
            PlayerPrefs.SetInt("beamCount", upgradeCount[1]);
            PlayerPrefs.SetInt("beamDmg", Beam.damage);
            PlayerPrefs.SetInt("beamCost", upgradeBeamCost + upgradeBeamCostScaled);

            GameManager.savePref();


            AudioManager.current.playUPGRADE();
        }
        else
        {
            AudioManager.current.playNEGATIVE();
        }

    }

    public void upgradeHomingMissile()
    {
		int upgradeHomingMissileCostScaled = upgradeHomingMissileCost + upgradeCount [2] * 100;

		if (GameManager.currency >= upgradeHomingMissileCostScaled)
        {
			GameManager.SubtractCurrency(upgradeHomingMissileCostScaled);
            HomingMissile.damage += upgradeHomingMissileDmg;
            homingMissileText.text = "DAMAGE: " + HomingMissile.damage.ToString();
            hmCost.text = "UPGRADE\n" + (upgradeHomingMissileCost + upgradeHomingMissileCostScaled).ToString() + " NRG";

            upgradeCount [2]++;
            PlayerPrefs.SetInt("hmCount", upgradeCount[2]);
            PlayerPrefs.SetInt("hmDmg", HomingMissile.damage);
            PlayerPrefs.SetInt("hmCost", upgradeHomingMissileCost + upgradeHomingMissileCostScaled);

            GameManager.savePref();

            AudioManager.current.playUPGRADE();
        }
        else
        {
            AudioManager.current.playNEGATIVE();
        }

    }
}

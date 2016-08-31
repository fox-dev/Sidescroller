﻿using UnityEngine;
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

	public int upgradeFireBallCost, upgradeBeamCost, upgradeHomingMissileCost;
	private int[] upgradeCount;

	public int upgradeFireBallDmg, upgradeBeamDmg, upgradeHomingMissileDmg;

    // Use this for initialization
    void Start () {

        fireBallText.text = "DAMAGE: " + ShootFireBall.damage.ToString();
        beamText.text = "DAMAGE: " + Beam.damage.ToString();
        homingMissileText.text = "DAMAGE: " + HomingMissile.damage.ToString();

		upgradeCount = new int[3];
		for (int i = 0; i < upgradeCount.Length; i++) {
			upgradeCount [i] = 0;
		}
    }

    public void upgradeFireBallDamage()
    {
		int upgradeFireBallCostScaled = upgradeBeamCost + upgradeCount [0] * 100;

        if (GameManager.currency >= upgradeFireBallCostScaled)
        {
            GameManager.SubtractCurrency(upgradeFireBallCostScaled);
			ShootFireBall.damage += upgradeFireBallDmg;
            fireBallText.text = "DAMAGE: " + ShootFireBall.damage.ToString();

			upgradeCount[0]++;
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

			upgradeCount[1]++;
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

			upgradeCount [2]++;
        }
            
    }
}

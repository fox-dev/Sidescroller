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

    // Use this for initialization
    void Start () {

        fireBallText.text = "DAMAGE: " + ShootFireBall.damage.ToString();
        beamText.text = "DAMAGE: " + Beam.damage.ToString();
        homingMissileText.text = "DAMAGE: " + HomingMissile.damage.ToString();
    }

    public void upgradeFireBallDamage()
    {
        int cost = 100;

        if (GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            ShootFireBall.damage += 10;
            fireBallText.text = "DAMAGE: " + ShootFireBall.damage.ToString();
        }
            

    }

    public void upgradeBeamDamage()
    {
        int cost = 100;

        if (GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            Beam.damage += 10;
            beamText.text = "DAMAGE: " + Beam.damage.ToString();
        }
  
    }

    public void upgradeHomingMissile()
    {
        int cost = 100;

        if (GameManager.currency >= cost)
        {
            GameManager.SubtractCurrency(cost);
            HomingMissile.damage += 10;
            homingMissileText.text = "DAMAGE: " + HomingMissile.damage.ToString();
        }
            
    }
}

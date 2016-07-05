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
	
	}
	
	// Update is called once per frame
	void Update () {
        fireBallText.text = "DAMAGE: " + ShootFireBall.damage.ToString();
        beamText.text = "DAMAGE: " + Beam.damage.ToString();
        homingMissileText.text = "DAMAGE: " + HomingMissile.damage.ToString();

    }

    public void upgradeFireBallDamage()
    {
        ShootFireBall.damage += 10;
    }

    public void upgradeBeamDamage()
    {
        Beam.damage += 10;
    }

    public void upgradeHomingMissile()
    {
        HomingMissile.damage += 10;
    }
}

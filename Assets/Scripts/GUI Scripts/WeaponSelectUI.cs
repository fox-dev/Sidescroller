using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WeaponSelectUI : MonoBehaviour {

    [SerializeField]
    private Player player;

    [SerializeField]
    private Button fireBall, beam, homingMissile;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        
    }
    
    void OnEnable()
    {
        if(GameManager.gm != null)
        {
            if (player.wep.projectile == GameManager.gm.weaponList[0])
            {
                fireBall.interactable = false;
                beam.interactable = true;
                homingMissile.interactable = true;
            }

            if (player.wep.projectile == GameManager.gm.weaponList[1])
            {
                fireBall.interactable = true;
                beam.interactable = false;
                homingMissile.interactable = true;
            }

            if (player.wep.projectile == GameManager.gm.weaponList[2])
            {
                fireBall.interactable = true;
                beam.interactable = true;
                homingMissile.interactable = false;
            }
        }
        
    }

	public void selectFireBall()
    {
        player.wep.projectile = GameManager.gm.weaponList[0]; //Fireball
        fireBall.interactable = false;
        beam.interactable = true;
        homingMissile.interactable = true;

        PlayerPrefs.SetString("CurrentWeapon", "FireBall");
    }

    public void selectBeam()
    {
        player.wep.projectile = GameManager.gm.weaponList[1]; //Beam
        fireBall.interactable = true;
        beam.interactable = false;
        homingMissile.interactable = true;

        PlayerPrefs.SetString("CurrentWeapon", "Beam");
    }

    public void selectHomingMissile()
    {
        player.wep.projectile = GameManager.gm.weaponList[2]; //HomingMissile
        fireBall.interactable = true;
        beam.interactable = true;
        homingMissile.interactable = false;
        PlayerPrefs.SetString("CurrentWeapon", "Homing");
    }
}

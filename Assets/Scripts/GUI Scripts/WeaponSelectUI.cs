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

	public void selectFireBall()
    {
        player.wep.projectile = GameManager.gm.weaponList[0]; //Fireball
        fireBall.interactable = false;
        beam.interactable = true;
        homingMissile.interactable = true;
    }

    public void selectBeam()
    {
        player.wep.projectile = GameManager.gm.weaponList[1]; //Beam
        fireBall.interactable = true;
        beam.interactable = false;
        homingMissile.interactable = true;
    }

    public void selectHomingMissile()
    {
        player.wep.projectile = GameManager.gm.weaponList[2]; //HomingMissile
        fireBall.interactable = true;
        beam.interactable = true;
        homingMissile.interactable = false;
    }
}

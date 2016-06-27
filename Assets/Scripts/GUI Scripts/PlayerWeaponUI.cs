using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerWeaponUI : MonoBehaviour {

    private GameObject player;
    private Text weaponText;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.LogError("Player object is not referenced correctly");
        }

        
        weaponText = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {

        weaponText.text = player.GetComponent<Player>().weapon.GetComponent<PlayerWeapon>().projectile.name;

     
	
	}
}

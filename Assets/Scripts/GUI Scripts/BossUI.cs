﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator))]
//BossUI is given parameters when boss is spawned in EnemySpawnManager
public class BossUI : MonoBehaviour {

    public static BossUI current;


    [SerializeField]
    public Animator bossGuiAnim;

    [SerializeField]
    public Image bossHealth;

    public Enemy boss;

    
	void Start () {
        current = this;

	    if(bossGuiAnim == null)
        {
            Debug.LogError("No bossGuiAnim referenced");
        }
	}
	
	// Update is called once per frame
	void Update () {

        if (GameManager.gm.state == GameManager.gameState.bossFight)
        {
         
            SetHealth(boss.stats.curHealth, boss.stats.maxHealth); //health set once enemy is queued in Enemy Spawn Manager, during gameState.PrepareForBoss

        }
        else
        {
         
            //bossHealth.fillAmount = 1; //refill
        }

    }

    public void SetHealth(int _cur, int _max)
    {
        float _value = (float)_cur / _max;

        bossHealth.fillAmount = _value;

        if (_value <= .45)
        {
            bossHealth.gameObject.GetComponent<Image>().color = Color.red;
        }
        else
        {
            bossHealth.gameObject.GetComponent<Image>().color = Color.green;
        }

    }
}

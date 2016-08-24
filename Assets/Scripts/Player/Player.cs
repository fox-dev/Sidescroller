using UnityEngine;
using System.Collections;

//Enemy class object to keep track of enemy's health and damage done to it, and which weapon is equipped;
public class Player : MonoBehaviour {


    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private bool _alive;
        public bool alive
        {
            get { return _alive; }
            set { _alive = value; }
        }
        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            curHealth = maxHealth;
            alive = true;
        }
    }

    public PlayerStats stats = new PlayerStats();

    [SerializeField]
    public GameObject model;
    [SerializeField]
    private GameObject weapon;

    public PlayerWeapon wep;

    public float blinkDuration;

    [SerializeField]
    private bool blinking;

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    

    void Start()
    {
        stats.Init();
        wep = weapon.GetComponent<PlayerWeapon>();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    public void DamagePlayer(int damage)
    {
        if (!blinking && !transform.GetComponent<PlayerMovement>().jumping)
        {
            Instantiate(Resources.Load("explosion"), transform.position, Quaternion.identity);

            stats.curHealth -= damage;
            //Add damage done to player to the gameStats of the GameManager
            GameManager.gm.gameStats.addTotalDamageTaken(damage);
            GameManager.gm.gameStats.addTimesHit();

            StartCoroutine(blink(blinkDuration, 0.2f));
        }
        else
        {
            //print("Currently invulnerable");
        }
        

        //print(stats.alive + " " + stats.curHealth);

        if (stats.curHealth <= 0 && stats.alive)
        {
            //gameObject.SetActive(false);

            GameManager.KillPlayer(this);

            //Damage player stuff here
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    IEnumerator blink(float duration, float blinkTime)
    {
        blinking = true;
        //Renderer o = gameObject.GetComponentInChildren<Renderer>();
       
        while (duration > 0f && stats.curHealth > 0)
        {
           // print(duration);
            duration -= Time.fixedDeltaTime;


            //o.enabled = !o.enabled;
            model.GetComponent<Renderer>().enabled = !model.GetComponent<Renderer>().enabled;
            model.GetComponent<TrailRenderer>().enabled = !model.GetComponent<TrailRenderer>().enabled;

            yield return new WaitForSeconds(blinkTime);

        }

        model.GetComponent<Renderer>().enabled = true;
        blinking = false;
    }

    public void respawn()
    {
        stats.Init();
        StartCoroutine(blink(blinkDuration, 0.2f));
    }


}

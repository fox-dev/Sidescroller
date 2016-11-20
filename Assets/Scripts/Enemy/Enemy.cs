using UnityEngine;
using System.Collections;

//Enemy class object to keep track of enemy's health and damage done to it;
[RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour {
    Renderer[] renderers;
    Color defaultColor;

    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        public int awardPoints = 100;

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
			maxHealth = maxHealth + (int)(maxHealth * (GameManager.difficulty / 3));
			curHealth = maxHealth;
            alive = true;

        }

		public void BossInit()
		{
			maxHealth = maxHealth + (int)(Mathf.Pow(maxHealth, GameManager.difficulty / 30));
			curHealth = maxHealth;
			alive = true;

		}
    }

    public EnemyStats stats = new EnemyStats();

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
		if (this.gameObject.tag == "Boss")
			stats.BossInit ();
		else
			stats.Init();
        
        renderers = GetComponentsInChildren<Renderer>();
        defaultColor = renderers[0].material.color;

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    void OnEnable()
    {
        stats.Init();

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }

    }

    public void DamageEnemy(int damage)
    {
        
        stats.curHealth -= damage;
        

        //Add damage done to this enemy to the gameStats of the GameManager
        if (stats.alive)
        {
            GameManager.gm.gameStats.addTotalDamageDone(damage);
            AudioManager.current.PlaySound("EnemyHit");
        }
        

        // print(stats.alive + " " + stats.curHealth);
        if (stats.curHealth <= 0 && stats.alive)
        {
           
            if (this.tag == "Boss")
            {
                //To play explosion animation properly, unparent the boss enemy object
                if(this.gameObject.transform.parent != null)
                {
                    this.gameObject.transform.parent = null;
                }
                stats.alive = stats.curHealth > 0;
                
                StartCoroutine(explode());
            }
            else
            {
                stats.alive = stats.curHealth > 0;
                Instantiate(Resources.Load("explosion"), transform.position, Quaternion.identity);
                GameManager.KillEnemy(this);
            }
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = defaultColor;
            }
        }
        else if(stats.curHealth > 0 && stats.alive)
        {
            StartCoroutine(damageFlash());
        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    IEnumerator explode()
    {
        GameManager.clearScreenOfProjectiles();
        GameObject temp = Instantiate(Resources.Load("Boom"), transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        yield return new WaitForSeconds(1.5f);
        Instantiate(Resources.Load("explosion2"), transform.position, Quaternion.identity);
        GameManager.KillEnemy(this);
    }

    IEnumerator damageFlash()
    {
        foreach(Renderer renderer in renderers)
        {
            renderer.material.color = Color.red;
        }
        yield return new WaitForSeconds(0.1f);
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = defaultColor;
        }
    }


}

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	[System.Serializable]
    public class EnemyStats
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

    public EnemyStats stats = new EnemyStats();

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    public void DamageEnemy(int damage)
    {
        stats.curHealth -= damage;
       // print(stats.alive + " " + stats.curHealth);
        if(stats.curHealth <= 0 && stats.alive)
        {
            if(this.tag == "Boss")
            {
                //To play explosion animation properly, unparent the boss enemy object
                if(this.gameObject.transform.parent != null)
                {
                    this.gameObject.transform.parent = null;
                }
                stats.alive = stats.curHealth > 0;
                print(stats.alive + " " + stats.curHealth);
                StartCoroutine(explode());
            }
            else
            {
                Instantiate(Resources.Load("explosion"), transform.position, Quaternion.identity);
                GameManager.KillEnemy(this);
            }
             

        }

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    IEnumerator explode()
    {
        GameObject temp = Instantiate(Resources.Load("Boom"), transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        yield return new WaitForSeconds(1.5f);
        Instantiate(Resources.Load("explosion2"), transform.position, Quaternion.identity);
        GameManager.KillEnemy(this);
    }


}

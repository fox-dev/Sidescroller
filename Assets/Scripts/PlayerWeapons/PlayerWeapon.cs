
using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

    public float timeBetweenBullets = 0.15f;
    public GameObject projectile; //Main weapon
    public GameObject superWeapon; //Special
    private GameObject origin;

    float nextBullet; //when next can be fired

    private bool fire;

    public float currentCharge, maxCharge; //charge amount, when full super can be used
    public bool charge; //to charge or not to charge
    public bool firing;
    public bool readyToFire;

    [Range(0,100)]
    public int chargeRate; //How fast the laser charges

    private bool occupied; //for couritine call to make sure it is only called once;

    private Player player;


    // Use this for initialization
    void Awake () {
        currentCharge = 0;
        maxCharge = 100;
        charge = true;
        firing = false;
        readyToFire = false;

        nextBullet = 0f;
        origin = GameObject.FindGameObjectWithTag("Origin");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        if ( ( Input.GetKey("e") || fire) && nextBullet < Time.time)
        {
            if(GameManager.gm.state == GameManager.gameState.setup)
            {
                fire = false;
            }

            if (projectile.name == "Player_Beam")
            {
                timeBetweenBullets = 0.15f;
                AudioManager.current.PlaySound("Beam");
            }
            else if (projectile.name == "Player_Fireball")
            {
                timeBetweenBullets = 0.15f;
                AudioManager.current.PlaySound("PlayerFire");
            }
            else if (projectile.name == "HomingMissile")
            {
                if(GameManager.gm.upgrades.rocketRate)
                {
                    timeBetweenBullets = 0.15f;
                }
                else
                {
                    timeBetweenBullets = 0.3f;
                }
                
                AudioManager.current.PlaySound("Rocket");
            }

            nextBullet = Time.time + timeBetweenBullets;

            Rigidbody bullet = ObjectPool.current.getPooledObjectRigidBody(projectile);

            if (bullet == null) return;

            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.transform.parent = origin.transform;
            bullet.gameObject.SetActive(true);

            if (!bullet.name.Contains("Homing"))
            {
                foreach (Transform child in bullet.transform)
                {

                    child.gameObject.SetActive(true);
                    child.GetComponent<Rigidbody>().AddForce(child.transform.forward * 100, ForceMode.Impulse);
                    

                }
            }

           
            
        }
      

        if (currentCharge < maxCharge && charge && ((GameManager.gm.state != GameManager.gameState.setup) && (GameManager.gm.state != GameManager.gameState.results) && (GameManager.gm.state != GameManager.gameState.waiting))) //Don't charge in transition states
        {

             currentCharge += (chargeRate * Time.deltaTime);
           
        }
        else if(firing)
        {
            if(currentCharge >= 0)
            {
                currentCharge -= (38f * Time.deltaTime);
            }

            if(currentCharge < 0)
            {
                currentCharge = 0;
            }
        }
       

        readyToFire = currentCharge >= maxCharge;

       
        /*
        if(gameObject.tag != "Buddy")
        {
            if (Input.GetKey("a"))
            {
                projectile = GameManager.gm.weaponList[0];
            }
            if (Input.GetKey("s"))
            {
                projectile = GameManager.gm.weaponList[1];
            }
        }
        */


    }
    public void fireSuper()
    {
        firing = true;

        AudioManager.current.playLASERCHARGE();

        player.invulFlag();

        ChargeMeterGUI.current.button.interactable = false;

        Rigidbody bullet = ObjectPool.current.getPooledObjectRigidBody(superWeapon);

        if (bullet == null) return;

        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;

        bullet.transform.parent = origin.transform;
        bullet.gameObject.SetActive(true);
        
    }

   

    //For onscreen button usage//
    public void firePressed()
    {
        if (GameManager.gm.state == GameManager.gameState.tutorial_1 || GameManager.gm.state == GameManager.gameState.tutorial_3) //Only usable when interactible during these states
        {
            if (TutorialOverlayUI.current.fire.interactable)
            {
                fire = true;
            }
        }
        else
        {
            fire = true;
        }
    }

    public void fireReleased()
    {
        fire = false;
    }

    public void togglePlayerInvul()
    {
        player.invulFlag();
    }

    


}

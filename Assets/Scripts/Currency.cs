using UnityEngine;
using System.Collections;

//Currency used for purchasing upgrades/etc
public class Currency : MonoBehaviour {

    [SerializeField]
    private Transform player; //Used for keeping track of player position

    private Transform myTransform;

    [SerializeField]
    public int amount;
   
    [SerializeField]
    private float flySpeed;

    private bool trackPlayer = false; //Used to delay the tracking of the player;

    public GameObject particle; //Collection particle

    private Rigidbody rb;

    // Use this for initialization
    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        myTransform = transform;
	
	}

    void OnDisable()
    {
        trackPlayer = false;
    }

    void OnEnable()
    {
        StartCoroutine(delayPlayerTracking());
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (trackPlayer)
        {

            myTransform.position = Vector3.MoveTowards(myTransform.position, player.position, flySpeed * Time.deltaTime);
        }
        else
        {
            Vector3 directionMid = (new Vector3(player.transform.position.x, player.transform.position.y, 0) - myTransform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(directionMid);
            transform.rotation = lookRotation;
            rb.AddForce(transform.forward * 20f);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
            GameObject hitEffect = ObjectPool.current.getPooledObject(particle);

            if (hitEffect == null) return;
            hitEffect.transform.position = other.transform.position;

            hitEffect.SetActive(true);

            GameManager.CollectCurrency(this);


        }
    }

    IEnumerator delayPlayerTracking()
    {
        
        yield return new WaitForSeconds(1.5f);
        trackPlayer = true;
        rb.velocity = Vector3.zero;
    }
}

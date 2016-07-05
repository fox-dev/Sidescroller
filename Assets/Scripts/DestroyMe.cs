using UnityEngine;
using System.Collections;

public class DestroyMe : MonoBehaviour {

    public float aliveTime;
    

	// Use this for initialization
	void Awake () {

        //Destroy(gameObject, aliveTime);
        StartCoroutine(disable());
    }

    void OnEnable()
    {
        Transform thisTransform = transform;

        if(!thisTransform.name.Contains("Homing"))
        {
            foreach (Transform child in thisTransform)
            {
                child.position = transform.position;
                child.rotation = transform.rotation;
                child.GetComponent<Rigidbody>().velocity = Vector3.zero;
                child.GetComponent<ParticleSystem>().Clear();
                child.GetComponent<ParticleSystem>().Play();
                child.gameObject.SetActive(true);
            }
        }
       
        
       // print(transform.position);
        StartCoroutine(disable());

    }
	

    IEnumerator disable()
    {
        yield return new WaitForSeconds(aliveTime);
        transform.position = Vector3.zero;
        gameObject.SetActive(false);
        
    }

  
}

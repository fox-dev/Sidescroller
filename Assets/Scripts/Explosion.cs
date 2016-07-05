using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Explosion : MonoBehaviour {

    GameObject flashed;
    Color c;
    

    float startValue, goToValue; //Values for the white image sprite opacity

	// Use this for initialization
	void Start () {
      
        GetComponent<AudioSource>().Play();
	}

    void Awake()
    {

        startValue = 0;
        flashed = GameObject.FindGameObjectWithTag("Flash");

        if (this.name.Contains("Explosion2"))
        {
            StartCoroutine(flash());
        }
        

        Destroy(this.gameObject, 3.5f); //destroy after 5 seconds.
    }

    void OnDisable()
    {

    }

    void Update()
    {
        
        if (this.name.Contains("Explosion2"))
        {
            flashWhite();
        }
        
    }

    void flashWhite()
    {
        //print(flashed.GetComponent<SpriteRenderer>().color.a);
        c =  flashed.GetComponent<SpriteRenderer>().color;
        c.a = Mathf.Lerp(c.a, goToValue, 2 * Time.deltaTime);
        if (c.a < 0.01)
        {
            c.a = 0;
        }
        flashed.GetComponent<SpriteRenderer>().color = c;
    }


    IEnumerator flash()
    {
        
        goToValue = 10f;
        yield return new WaitForSeconds(0.1f);

        goToValue = 0f;

        flashed.GetComponent<SpriteRenderer>().color = c;
        GameObject light = GameObject.FindGameObjectWithTag("Light");
        light = GameObject.FindGameObjectWithTag("Light");
        light.GetComponent<Light>().intensity = 0.8f;
        

    }

    
	
}

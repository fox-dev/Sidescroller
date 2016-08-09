using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Explosion : MonoBehaviour {

    GameObject flashed;
    Color c;

    private float updateVal = 0;

    float startValue, goToValue; //Values for the white image sprite opacity

	// Use this for initialization
	void Start () {
      
        GetComponent<AudioSource>().Play();
        Destroy(this.gameObject, 3f);
	}

    void Awake()
    {

        startValue = 0;
        flashed = GameObject.FindGameObjectWithTag("Flash");

        if (this.name.Contains("Explosion2"))
        {
            StartCoroutine(flash());
        }
        

        //Destroy(this.gameObject, 8.5f); //destroy after 5 seconds.
    }

    void OnDisable()
    {
        goToValue = 10f;
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
        c.a = Mathf.Lerp(c.a, goToValue, 2f * Time.deltaTime);
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
        light.GetComponent<Light>().intensity = 1f;
        

    }

    
	
}

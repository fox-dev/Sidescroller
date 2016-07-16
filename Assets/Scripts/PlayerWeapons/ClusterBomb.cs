using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClusterBomb : MonoBehaviour
{

    GameObject flashed;
    Color c;

    public GameObject clusterExplosions;

    private ParticleSystem ps;

    private float startSize;
    private float endSize;

    private bool occupied = false; //for coroutine use in update;
    private bool clustering = false; //indicate when cluster growth starts


    float startValue, goToValue; //Values for the white image sprite opacity

    // Use this for initialization
    void Start()
    {
        

        GetComponent<AudioSource>().Play();
    }

    void Awake()
    {

        startValue = 0;
        flashed = GameObject.FindGameObjectWithTag("Flash");
        ps = clusterExplosions.GetComponent<ParticleSystem>();
        startSize = 2.5f;
        endSize = 15f;

        if (this.name.Contains("Explosion2"))
        {
            StartCoroutine(flash());
        }


        StartCoroutine(countdown());
        StartCoroutine(beginClusterGrowth());
        


        Destroy(this.gameObject, 10.5f); //destroy after 5 seconds.
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
        if (!occupied && clustering)
        {
            StartCoroutine(repeatedBlastSound());
        }
        

    }

    void flashWhite()
    {
        //print(flashed.GetComponent<SpriteRenderer>().color.a);
        c = flashed.GetComponent<SpriteRenderer>().color;
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
        light.GetComponent<Light>().intensity = 1f;


    }

    IEnumerator countdown()
    {
        yield return new WaitForSeconds(8f);
        clustering = false;
    }

    IEnumerator repeatedBlastSound()
    {
        occupied = true;

        yield return new WaitForSeconds(0.2f);

        GetComponent<AudioSource>().Play();
        occupied = false;
    }

    IEnumerator beginClusterGrowth()
    {
        yield return new WaitForSeconds(1.5f);

        clustering = true;
        float timeToStart = Time.time;
        while(ps.startSize <= endSize)
        {
            ps.startSize += 0.04f;

            yield return null;
        }

        
    }



}

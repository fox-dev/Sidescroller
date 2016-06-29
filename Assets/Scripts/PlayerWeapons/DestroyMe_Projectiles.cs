using UnityEngine;
using System.Collections;

public class DestroyMe_Projectiles : MonoBehaviour
{

    public float aliveTime;


    // Use this for initialization
    void Awake()
    {
       
        //Destroy(gameObject, aliveTime);
        StartCoroutine(disable());
    }

    void OnEnable()
    {
        //Renderer rend = GetComponent<Renderer>();

        //rend.material.color = new Color(248, 251, 22);
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

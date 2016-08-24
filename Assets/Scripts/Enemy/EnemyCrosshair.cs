using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
//MIRROR BOSS CROSSHAIR
public class EnemyCrosshair : MonoBehaviour
{

    public GameObject crosshair;
    private Enemy enemy;
    private Transform myTransform;
    int z = 0;
    // Use this for initialization
    void Start()
    {
        myTransform = transform;
        enemy = myTransform.parent.GetComponent<Enemy>();

        myTransform.parent = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!enemy.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false); //If mirrorboss dies, set the crosshair inactive;
        }

        crosshair.transform.rotation = Quaternion.Euler(0, 0, z += 3);

    }
 

    public Transform getPosition()
    {
        return this.transform;
    }





}

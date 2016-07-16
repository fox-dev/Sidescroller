using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemyCrosshair : MonoBehaviour
{

    public GameObject crosshair;
    private Transform myTransform;
    int z = 0;
    // Use this for initialization
    void Start()
    {
        myTransform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        crosshair.transform.rotation = Quaternion.Euler(0, 0, z += 3);
    }
 

    public Transform getPosition()
    {
        return this.transform;
    }





}

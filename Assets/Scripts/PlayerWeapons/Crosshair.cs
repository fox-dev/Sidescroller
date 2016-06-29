﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Crosshair : MonoBehaviour {

    public GameObject crosshair;
    public Canvas canvas;
    private Transform myTransform;
    int z = 0;
	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        crosshair.transform.rotation = Quaternion.Euler(0, 0, z++);
    }
	void Update () {
        Ray ray;
        float z_plane_of_2d_game = 0;
        Vector3 pos_at_z_0;
   
            
        if(Input.touchCount > 0)
        {
            if (!IsPointerOverUIObject(canvas, Input.GetTouch(0).position))
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                z_plane_of_2d_game = 0;
                pos_at_z_0 = ray.origin + ray.direction * (z_plane_of_2d_game - ray.origin.z) / ray.direction.z;
                myTransform.position = pos_at_z_0;

            }
        }
        
  

       
     
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            z_plane_of_2d_game = 0;
            pos_at_z_0 = ray.origin + ray.direction * (z_plane_of_2d_game - ray.origin.z) / ray.direction.z;

            myTransform.position = pos_at_z_0;
        }
        
        
    }

    bool IsPointerOverGameObject(int fingerId)
    {
        EventSystem eventSystem = EventSystem.current;
        return (eventSystem.IsPointerOverGameObject(fingerId)
            && eventSystem.currentSelectedGameObject != null);
    }

    private bool IsPointerOverUIObject(Canvas canvas, Vector2 screenPosition)
    {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;

        GraphicRaycaster uiRaycaster = canvas.gameObject.GetComponent<GraphicRaycaster>();
        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    public Transform getPosition()
    {
        return this.transform;
    }

    
 
       
    
}

using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {

    public GameObject crosshair;
    int z = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float z_plane_of_2d_game = 0;
        Vector3 pos_at_z_0 = ray.origin + ray.direction * (z_plane_of_2d_game - ray.origin.z) / ray.direction.z;
        transform.position = pos_at_z_0;
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            z_plane_of_2d_game = 0;
            pos_at_z_0 = ray.origin + ray.direction * (z_plane_of_2d_game - ray.origin.z) / ray.direction.z;

            transform.position = pos_at_z_0;
        }

        crosshair.transform.rotation = Quaternion.Euler(0, 0, z++);
       
    }


    public Transform getPosition()
    {
        return this.transform;
    }
}

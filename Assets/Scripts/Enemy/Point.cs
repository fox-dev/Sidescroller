using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;

    private GameObject player;

    Vector3 target;

    Quaternion targetRot;

    Quaternion current;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targetRot = Quaternion.Euler(0, 0, 0);
        current = this.transform.rotation;


    }

    void OnEnable()
    {
        current = this.transform.rotation;
    }



    // Update is called once per frame
    void LateUpdate()
    {
 
        float AngleRad = Mathf.Atan2(player.transform.position.y - transform.position.y, player.transform.position.x - transform.position.x);

        float AngleDeg = (180 / Mathf.PI) * AngleRad;

        targetRot = Quaternion.Euler(0, 0, AngleDeg + moveSpeed);

        
        current = Quaternion.Slerp(current, targetRot, 3f * Time.deltaTime);
        this.transform.rotation = current;

        print(AngleDeg);
       


    }




}





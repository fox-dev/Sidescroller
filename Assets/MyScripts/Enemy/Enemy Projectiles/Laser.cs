using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{
    GameObject player;

    public float range = 10f;
    public float damage = 5;
    public float maxWidth = 7f;
    public float startWidth = 0.025f;
    public float growingWidth = 0.025f;

    public float startWidth2 = 7f;
    public float growingWidth2 = 7f;

    Ray shootRay;
    RaycastHit shootHit;
    RaycastHit2D hit;
    public LayerMask shootableMask;
    LineRenderer gunLine;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        Vector2 point = new Vector2(transform.position.x, player.transform.position.y);
        ////////////////

        gunLine = GetComponent<LineRenderer>();

        //Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        //Vector3 mousePos3 = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);

        Vector2 currentPos = new Vector2(transform.position.x, transform.position.y);
        //Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);


        shootRay.origin = transform.position;
        shootRay.direction = (point - currentPos);
        //shootRay.direction = transform.forward;

        gunLine.SetPosition(0, transform.position);


        //hit = Physics2D.Raycast(currentPos, mousePos - currentPos, range, shootableMask);
        // Physics.Raycast(currentPos, mousePos - currentPos, out hit, 100, shootableMask);


        //Debug.DrawLine(currentPos, (mousePos - currentPos) * range);


        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            //hit an enemy goes here
            gunLine.SetPosition(1, shootHit.point);
            Instantiate(Resources.Load("explosion"), shootHit.transform.position, Quaternion.identity);
            if (shootHit.transform.gameObject.layer != LayerMask.NameToLayer("Road"))
            {
                shootHit.transform.gameObject.SetActive(false);
            }
            print("HITHITHIT");

        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);


        }



    }

    // Update is called once per frame
    void Update()
    {




        if (growingWidth <= maxWidth - 1f)
        {
            growingWidth = Mathf.Lerp(growingWidth, maxWidth, Time.deltaTime * 10f);
            gunLine.SetWidth(startWidth, growingWidth);
        }
        else if (growingWidth >= maxWidth - 1f)
        {
            growingWidth2 = Mathf.Lerp(growingWidth2, 0, Time.deltaTime * 10f);
            gunLine.SetWidth(startWidth, growingWidth2);
        }



    }

}

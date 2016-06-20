using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerController))]
public class PlayerMovement : MonoBehaviour {

    public float jumpHeight = 3.5f;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .1f;
    float accelerationTimeGrounded = .1f;
    public float moveSpd = 0f;

    float gravity;
    float jumpVelocity;
    Vector3 velocity, groundedVelocity;
    float velocityXSmoothing;

    PlayerController controller;

    
    public float speed_Up =  40f;
    public float speed_Down = -25;
    public GameObject origin, origin2;
    public bool maxed_Up, maxed_Down = false;
    public bool moveUp = false;
    bool desc;
   


	// Use this for initialization
	void Start () {
        desc = false;
        controller = GetComponent<PlayerController>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    /*
	// Update is called once per frame
	void Update () {

        if (Input.GetKey("w"))
        {
            transform.Translate((Vector3.forward) * moveSpd * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate((Vector3.left) * moveSpd * Time.deltaTime);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate((Vector3.back) * moveSpd * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate((Vector3.right) * moveSpd * Time.deltaTime);
        }

        // transform.Translate((Vector3.left) * moveSpd * Time.deltaTime);

    }

    */

    void Update()
    {
        groundedVelocity = velocity;
        groundedVelocity.y = 0;



        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;

        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;

        }


        // print(transform.position.x - origin.transform.position.x);
        if (controller.collisions.descendingSlope && origin2.GetComponent<OriginController>().collisions.descendingSlope)
        {
            desc = true;
        }
        else if(!controller.collisions.descendingSlope && !origin2.GetComponent<OriginController>().collisions.descendingSlope)
        {
            desc = false;
        }



        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(transform.position.x, origin.transform.position.x, origin2.transform.position.x);
        if (origin.GetComponent<OriginController>().collisions.climbingSlope || origin2.GetComponent<OriginController>().collisions.climbingSlope)
        {
            clampedPosition.y = Mathf.Clamp(transform.position.y, origin.transform.position.y, Mathf.Infinity); //for climbing slopes, to prevent falling through terrain
        }
     
        transform.position = clampedPosition;

       

        Vector3 dir1 = origin.transform.position - transform.position;
        Vector3 dir2 = origin2.transform.position - transform.position;

        if (Input.GetKey("d"))
        {

            moveUp = true;
            maxed_Down = false;

            if (!maxed_Up)
            {

                velocity.x = speed_Up;
                
 
            }

            if (transform.position.x >= origin2.transform.position.x) 
            {
                
                maxed_Up = true;
                //velocity = Vector3.zero;
                //moveSpd = 15f;
                moveSpd = GameManager.gm.moveSpeed;
                //velocity = new Vector3(moveSpd, velocity.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(origin2.transform.position.x, transform.position.y, transform.position.z), 10 * Time.deltaTime);

            }
         
        }
        
        else
        {
            moveUp = false;
            maxed_Up = false;


            if (!maxed_Down)
            {


                velocity.x = speed_Down;
                
                
        
            }
            
            if (transform.position.x <= origin.transform.position.x)
            {

                maxed_Down = true;
                
                
                velocity = new Vector3(moveSpd, velocity.y, 0);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(origin.transform.position.x, transform.position.y, transform.position.z), 100 * Time.deltaTime);
            }
            

        }

       //print(controller.collisions.descendingSlope + " " + controller.collisions.climbingSlope);


        if (controller.collisions.below)
        {
            //float targetVelocityX_input = input.x * moveSpd;
            float targetVelocityX_input = moveSpd;
            //float targetVelocityX = Mathf.Sin(controller.collisions.currentSlopeAngle * Mathf.Deg2Rad) * -gravity; // Speed based of slope;
           //velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX + targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);

            controller.collisions.speedBeforeJump = velocity.x;
        }
        else
        {
            //float targetVelocityX_input = input.x * moveSpd;
            //float targetVelocityX_input = controller.collisions.speedBeforeJump/1.75f;
            float targetVelocityX_input = controller.collisions.speedBeforeJump/1.75f;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX_input, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        }


       
       velocity.y += gravity * Time.deltaTime;
       controller.Move(velocity * Time.deltaTime);

        
    }

    public Vector3 getVelocity()
    {
        return velocity;
    }

    public Vector3 getGroundedVelocity()
    {
        return groundedVelocity;
    }
}
